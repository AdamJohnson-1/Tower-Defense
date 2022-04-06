using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ActionHandlerScript : MonoBehaviour
{
    public static ActionHandlerScript ActionHandlerScriptInstance = null;

    private ActionState currentState;
    private TowerHandler towerHandler;

    public ErrorMessageScript errorMessageScript;

    public Canvas towerPlacementTip;
    public Material invalidTowerPlacementMaterial;
    public Material validTowerPlacementMaterial;

    public GameObject SelectBorder;

    void Start()
    {
        ActionHandlerScript.ActionHandlerScriptInstance = this;

        towerHandler = gameObject.GetComponent<TowerHandler>();
        currentState = new NoneState(this, SelectBorder);
    }

    public void selectTowerToPlace(GameObject tower, GameObject holo)
    {
        if (GameObject.ReferenceEquals(tower, null) || GameObject.ReferenceEquals(holo, null))
            return;
        ActionState newState = new TowerPlacingState(this, tower, holo);
        currentState.leaveState(newState);
        currentState = newState;
    }
    public void deselectTowerToPlace()
    {
        ActionState newState = new NoneState(this, SelectBorder);
        currentState.leaveState(newState);
        currentState = newState;
    }

    public void selectTowerToEdit(GameObject nodeObj)
    {
        ActionState newState = new TowerSelectedState(this, SelectBorder, nodeObj);
        currentState.leaveState(newState);
        currentState = newState;
        SelectBorder.SetActive(true);
    }
    public void deselectTowerToEdit()
    {
        ActionState newState = new NoneState(this, SelectBorder);
        currentState.leaveState(newState);
        currentState = newState;
    }


    public void onMouseEnterNode(GameObject nodeObject)
    {
        currentState.onMouseEnterNode(nodeObject);
    }
    public void onMouseLeaveNode(GameObject nodeObject)
    {
        currentState.onMouseLeaveNode(nodeObject);
    }
    public void onLeftMouseDown()
    {
        currentState.onLeftMouseDown();
    }
    public void onRightMouseDown()
    {
        currentState.onRightMouseDown();
    }
    
    
    

    abstract class ActionState
    {

        protected ActionHandlerScript parentScript;
        public ActionState(ActionHandlerScript script)
        {
            parentScript = script;
        }

        public abstract void leaveState(ActionState newState);
        public abstract void onMouseEnterNode(GameObject nodeObject);
        public abstract void onMouseLeaveNode(GameObject nodeObject);
        public abstract void onLeftMouseDown();
        public abstract void onRightMouseDown();
    }

    class TowerSelectedState : ActionState
    {
        private GameObject SelectBorder;
        private GameObject nodeObj;
        private GameObject mousedOverNode;

        public TowerSelectedState(ActionHandlerScript script, GameObject _SelectBorder, GameObject _nodeObj) : base(script)
        {
            SelectBorder = _SelectBorder;
            nodeObj = _nodeObj;
            mousedOverNode = _nodeObj;

            if (UpgradeTowerScript.UpgradeTowerScriptInstance == null)
            {
                Debug.Log("UpgradeTowerBox must be active when the game starts - its Start() must be run.");
            }

            UpgradeTowerScript.UpgradeTowerScriptInstance.SelectTower(nodeObj);
        }

        public override void leaveState(ActionState newState)
        {
            SelectBorder.SetActive(false);
            
        }
        public override void onMouseEnterNode(GameObject nodeObject)
        {
        }
        public override void onMouseLeaveNode(GameObject nodeObject)
        {
        }
        public override void onLeftMouseDown()
        {
        }
        public override void onRightMouseDown()
        {
        }
    }

    class TowerPlacingState : ActionState
    {

        private GameObject nodeGameObject = null;

        private GameObject tower;
        private GameObject towerHologram;

        private static Color validTowerColor = new Color(0f, 0.77f, 0.2f, 0.3f);
        private static Color invalidTowerColor = new Color(0.9f, 0f, 0f, 0.3f);

        private float towerRange;//placeholder until we get abstracted tower ranges
        private int towerCost;

        public TowerPlacingState(ActionHandlerScript script, GameObject towerArg, GameObject holo) : base(script)
        {
            parentScript.towerPlacementTip.gameObject.SetActive(true);
            tower = towerArg;

            towerCost = tower.GetComponent<AttackTower>().GetTowerPrice();
            towerRange = tower.GetComponent<AttackTower>().GetDefaultRange();

            towerHologram = GameObject.Instantiate(holo);
            towerHologram.SetActive(false);

        }

        public override void leaveState(ActionState newState)
        {
            if(!GameObject.ReferenceEquals(nodeGameObject, null))
            {
                newState.onMouseEnterNode(nodeGameObject);
                onMouseLeaveNode(nodeGameObject);
            }


            parentScript.towerPlacementTip.gameObject.SetActive(false);
            GameObject.Destroy(towerHologram);
        }

        public override void onMouseEnterNode(GameObject nodeObjectArg)
        {
            nodeGameObject = nodeObjectArg;
            towerHologram.SetActive(true);

            //checks if placement is valid and if you have enough money
            Renderer[] holoRenderers = towerHologram.GetComponentsInChildren<Renderer>();
            if (parentScript.towerHandler.checkIfValidTowerLocation(nodeGameObject) &&
                Shop.checkIfMoneyGreaterOrEqual(towerCost))
            {
                foreach (Renderer holoRenderer in holoRenderers)
                {
                    holoRenderer.material = parentScript.validTowerPlacementMaterial;
                }
            } else
            {
                foreach (Renderer holoRenderer in holoRenderers)
                {
                    holoRenderer.material = parentScript.invalidTowerPlacementMaterial;
                }
            }

            //places hologram
            Vector3 nodePosition = nodeObjectArg.transform.position;
            float x = nodePosition.x;
            float y = nodePosition.y;
            float z = nodePosition.z;

            towerHologram.transform.position = new Vector3(
                x + TowerHandler.relativeTowerPosX,
                y + TowerHandler.relativeTowerPosY,
                z + TowerHandler.relativeTowerPosZ);
        }


        public override void onMouseLeaveNode(GameObject nodeObjectArg)
        {

            if (GameObject.ReferenceEquals(nodeObjectArg, nodeGameObject))
            {
                towerHologram.SetActive(false);
                nodeGameObject = null;
            }
        }

        public override void onLeftMouseDown()
        {
            //so the player doesn't accidentially place a turret through the User Interface
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            //returns if player's cursor is not on a node
            if (GameObject.ReferenceEquals(nodeGameObject, null))
            {
                parentScript.deselectTowerToPlace();
                return;
            }

            //returns if for some reason a tower isn't selected
            if (GameObject.ReferenceEquals(tower, null))
            {
                parentScript.deselectTowerToPlace();
                return;
            }

            attemptPlaceTower();
        }

        private void attemptPlaceTower()
        {
            //checks if player has engouh money
            if (!Shop.checkIfMoneyGreaterOrEqual(towerCost))
            {
                parentScript.errorMessageScript.ShowMessage("Not enough money.");
                return;
            }

            //checks if location is valid
            if (!parentScript.towerHandler.checkIfValidTowerLocation(nodeGameObject))
            {
                parentScript.errorMessageScript.ShowMessage("Invalid tower location.");
                return;
            }

            placeTower();
        }

        private void placeTower()
        {
            //adds tower and reports any errors from towerHandler
            if (!parentScript.towerHandler.addTower(tower, nodeGameObject))
            {
                parentScript.errorMessageScript.ShowMessage("Could not place tower.");
                return;
            }

            //finally subtracts tower cost and deselects tower if player isn't pressing shift
            Shop.changeMoney(-towerCost);
            if (!Input.GetKey(KeyCode.RightShift) &&
                !Input.GetKey(KeyCode.LeftShift))
            {
                parentScript.deselectTowerToPlace();
                return;
            }

            //"leaves" the node so a red hologram doesn't immediately appear
            onMouseLeaveNode(nodeGameObject);
        }

        public override void onRightMouseDown()
        {
            parentScript.deselectTowerToPlace();
        }
    }

    class NoneState : ActionState
    {

        public GameObject SelectBorder;
        private GameObject CurrentNode = null;

        public NoneState(ActionHandlerScript script, GameObject _SelectBorder) : base(script)
        {
            SelectBorder = _SelectBorder;
        }

        public override void leaveState(ActionState newState)
        {
            if (SelectBorder.activeSelf)
            {
                SelectBorder.SetActive(false);
            }
        }
        public override void onMouseEnterNode(GameObject nodeObject)
        {
            if (!parentScript.towerHandler.checkIfValidTowerLocation(nodeObject))
            {
                SelectBorder.SetActive(true);

                //places hologram
                Vector3 nodePosition = nodeObject.transform.position;
                float x = nodePosition.x;
                float y = nodePosition.y;
                float z = nodePosition.z;

                SelectBorder.transform.position = new Vector3(
                    x + TowerHandler.relativeTowerPosX,
                    y + TowerHandler.relativeTowerPosY,
                    z + TowerHandler.relativeTowerPosZ);

                CurrentNode = nodeObject;
            }
        }
        public override void onMouseLeaveNode(GameObject nodeObject)
        {
            if (SelectBorder.activeSelf)
            {
                SelectBorder.SetActive(false);
                CurrentNode = null;
            }
        }
        public override void onLeftMouseDown()
        {
            if(CurrentNode != null)
            {
                parentScript.selectTowerToEdit(CurrentNode);
            }
        }
        public override void onRightMouseDown()
        {
        }
    }
}

