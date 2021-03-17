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

    private Transform player;
    private String PropertyType, CollectableType;
    public int DocumentIndex;
    private Vector3 DesiredY;
    private Vector3 rotate;
    private float mPosX;
    private float mPosY;
    private float rotationx;
    private float rotationy;

    public DocumentManager DocMan;

    private void Start()
    {
        PropertyType = transform.GetChild(0).tag;

        player = GameObject.Find("Main Camera").transform;
       
        #region LIST_MANAGING
        PropertyList.Add("Holdable");
        PropertyList.Add("Collectable");
        PropertyList.Add("Readable");
        CollectableList.Add("Compass");
        CollectableList.Add("Weapon");
        ReadableList.Add("FirstDocument");
        #endregion
        
        if (PropertyType == PropertyList[1])
        {
            CollectableType = transform.GetChild(1).tag;
        }
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
        if (PropertyType == PropertyList[0]) //PropertyList[0] --- Holdable;
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
        if (PropertyType == PropertyList[1]) //PropertyList[1] --- Collectable;
        {
            if(CollectableType == CollectableList[0]) //CollectableList[0] --- Compass;
            {
                GameGlobeData.isCompassCollected = true;
                Destroy(gameObject);
            }
            
        }
        #endregion

        #region READABLE
        if(PropertyType == PropertyList[2]) //PropertyList[2] --- Readable;
        {
            GameGlobeData.IsDocumentCollected = true;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                GameGlobeData.IsDocumentCollected = false;
                hasInteracted = false;
            }

            if (DocumentIndex == 0)
            {
               var title = "Documento #1 = Dona Maria virou cinzas";
               var body = "A vida � realmente uma caixinha de surpresas! Quando imaginei ter me livrado de uma boa dor de cabe�a, Anne, a faxineira do edif�cio, me vem com essa de que os dirigentes da empresa me viram passando informa��es privilegiadas para ajudar a libera��o do r�u! Isso � um absurdo! Preciso sair desse lugar imediatamente, antes que o pior aconte�a. Estou realmente preocupado pela minha sa�de e sanidade. Espero sair desse lugar com vida...";
               DocMan.ChangeDocument(title, body);
            }

            if (DocumentIndex == 1)
            {
                var title = "Documento #2 = Bolsonaro Genocida";
                var body = "� indiscut�vel, no cen�rio atual de toda a esfera s�cio-politica brasileira, que a conduta e a postura do atual presidente da rep�blica, Jair Bolsonaro, s�o �ndices indisc�tiveis de um GENOCIDA. Eu estou escrevendo isso de placeholder pro segundo documento, mas a verdade � que enquanto desenvolvo esse projeto, meu cora��o se enche duvidas e incertezas sobre o meu futuro e o futuro do pa�s com um governante t�o bo�al, desprepado e ignorante. N�o apenas o governante em si, mas ver uma consider�vel massa apoiadora de um chorume desses me faz cada vez pensar que n�o temos salva��o. � isso amigos. Obrigado por ler at� aqui.";
                DocMan.ChangeDocument(title, body);
            }
           
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
