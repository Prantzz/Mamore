using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class objectController : MonoBehaviour
{
    public bool isSelected = false;
    public bool hasInteracted = false;
    public event EventHandler OnSelection;
    public event EventHandler OnInteraction;
    [SerializeField] private float zeroY;
    public List<String> PropertyList;
    public List<String> CollectableList;
    public List<String> ReadableList;

    private Transform PropertyType, player;

    private Vector3 DesiredY;
    private Vector3 rotate;
    private float mPosX;
    private float mPosY;
    private float rotationx;
    private float rotationy;

    private void Start()
    {
        PropertyType = transform.GetChild(0);
        player = GameObject.Find("Main Camera").transform;
       
        #region LIST_MANAGING
        PropertyList.Add("Holdable");
        PropertyList.Add("Collectable");
        PropertyList.Add("Readable");
        #endregion
    }
    private void Update()
    {
        if (isSelected) OnSelection?.Invoke(this, EventArgs.Empty);
        OnSelection += ObjectController_OnSelection;
        if (hasInteracted) OnInteraction?.Invoke(this, EventArgs.Empty);
        OnInteraction += ObjectController_OnInteraction;

        KeepObjectAboveGround();
        UpdateMouseValues();      
    }

    #region EVENT_METHODS
    private void ObjectController_OnSelection(object sender, EventArgs e)
    {
        if (Input.GetKeyDown(KeyCode.E) && !hasInteracted) 
        {
            hasInteracted = true;
        }
    }

    private void ObjectController_OnInteraction(object sender, EventArgs e)
    {
        #region HOLDABLE
        if (PropertyType.CompareTag(PropertyList[0]))
        {
            if (Input.GetKey(KeyCode.E))
            {
                gameObject.transform.parent = player.transform;
                gameObject.GetComponent<Rigidbody>().isKinematic = true;

                if (Input.GetKey(KeyCode.Q))
                {
                    GameGlobeData.IsCamLock = true;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = false;

                    rotationx -= mPosY;
                    rotationy -= mPosX;


                    transform.Rotate(Vector3.up * mPosX);
                    transform.Rotate(Vector3.right * mPosY);
                    transform.localRotation = Quaternion.Euler(-rotationx, rotationy, 0);
                }
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                gameObject.transform.parent = null;
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
                hasInteracted = false;
                GameGlobeData.IsCamLock = false;
                Cursor.lockState = CursorLockMode.Locked;
            }

            if (Input.GetKeyUp(KeyCode.Q))
            {
                GameGlobeData.IsCamLock = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        #endregion
        #region COLLECTABLE
        if (PropertyType.CompareTag(PropertyList[1]))
        {
            Destroy(gameObject);
        }
        #endregion
    }


    #endregion

    #region UPDATE_METHODS
    private void KeepObjectAboveGround()
    {
        if (transform.position.y <= zeroY) transform.position = new Vector3(transform.position.x, zeroY, transform.position.z);
    }

    private void UpdateMouseValues() 
    {
        mPosX = Input.GetAxis("Mouse X") * 0.2f * Time.deltaTime;
        mPosY = Input.GetAxis("Mouse Y") * 0.2f * Time.deltaTime;
    }
    #endregion

}
