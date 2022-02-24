using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ActionHandlerScript : MonoBehaviour
{
    private ActionState currentState;
    private TowerHandler towerHandler;

    public ErrorMessageScript errorMessageScript;
    public Shop shopScript;

    public Canvas towerPlacementTip;
    public Material invalidTowerPlacementMaterial;
    public Material validTowerPlacementMaterial;

    void Start()
    {
        towerHandler = gameObject.GetComponent<TowerHandler>();
        currentState = new NoneState(this);
    }

    public void selectTowerToPlace(GameObject tower, GameObject holo)
    {
        if (GameObject.ReferenceEquals(tower, null) || GameObject.ReferenceEquals(holo, null))
            return;
        currentState = new TowerPlacingState(this, tower, holo);
    }
    public void deselectTowerToPlace()
    {
        ActionState newState = new NoneState(this);
        currentState.leaveState(newState);
        currentState = new NoneState(this);
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

    class TowerPlacingState : ActionState
    {
        public GameObject test;

        private GameObject tower;
        private GameObject nodeGameObject;

        private GameObject towerHologram;
        private static Color validTowerColor = new Color(0f, 0.77f, 0.2f, 0.3f);
        private static Color invalidTowerColor = new Color(0.9f, 0f, 0f, 0.3f);

        //private float towerRange = 20;//placeholder until we get abstracted tower ranges
        private int towerCost = 10;//placeholder until we get abstracted tower costs

        public TowerPlacingState(ActionHandlerScript script, GameObject towerArg, GameObject holo) : base(script)
        {
            parentScript.towerPlacementTip.gameObject.SetActive(true);
            tower = towerArg;

            towerHologram = GameObject.Instantiate(holo);
            towerHologram.SetActive(false);
            //towerHologramRenderer = towerHologram.GetComponent<Renderer>();
            //towerHologramRenderer.material.color = TowerPlacingState.validTowerColor;

    }

        public override void leaveState(ActionState newState)
        {
            if(!GameObject.ReferenceEquals(nodeGameObject, null))
                onMouseLeaveNode(nodeGameObject);


            parentScript.towerPlacementTip.gameObject.SetActive(false);
            GameObject.Destroy(towerHologram);
        }

        public override void onMouseEnterNode(GameObject nodeObjectArg)
        {
            nodeGameObject = nodeObjectArg;
            towerHologram.SetActive(true);

            //checks if placement is valid and if you have enough money
            Renderer holoRenderer = towerHologram.GetComponentInChildren<Renderer>();
            if (parentScript.towerHandler.checkIfValidTowerLocation(nodeGameObject) &&
                parentScript.shopScript.checkIfMoneyGreaterOrEqual(towerCost))
            {
                holoRenderer.material = parentScript.validTowerPlacementMaterial;
            } else
            {
                holoRenderer.material = parentScript.invalidTowerPlacementMaterial;
            }

            //places hologram
            Vector3 nodePosition = nodeObjectArg.transform.position;
            float x = nodePosition.x;
            float y = nodePosition.y;
            float z = nodePosition.z;

            towerHologram.transform.position = new Vector3(
                x + 1,
                y + 1,
                z - 1);
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
                return;

            //returns if player's cursor is not on a node
            if (GameObject.ReferenceEquals(tower, null))
                return;

            attemptPlaceTower();
        }

        private void attemptPlaceTower()
        {
            //checks if player has engouh money
            if (!parentScript.shopScript.checkIfMoneyGreaterOrEqual(towerCost))
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

            //finally subtracts tower cost and deselects tower if player can't buy any more.
            parentScript.shopScript.changeMoney(-towerCost);
            if (!Input.GetKey(KeyCode.RightShift) &&
                !Input.GetKey(KeyCode.LeftShift))
            {
                parentScript.deselectTowerToPlace();
                return;
            }

            onMouseLeaveNode(nodeGameObject);
        }

        public override void onRightMouseDown()
        {
            parentScript.deselectTowerToPlace();
        }
    }

    class NoneState : ActionState
    {
        public NoneState(ActionHandlerScript script) : base(script)
        {
        }

        public override void leaveState(ActionState newState)
        {
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
}

