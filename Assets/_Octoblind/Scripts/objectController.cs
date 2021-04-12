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
    public int AudioNum;

    private Transform player;
    private String PropertyType, CollectableType;
    public int DocumentIndex;
    private Vector3 DesiredY;
    private Vector3 rotate;
    private float mPosX;
    private float mPosY;
    private float rotationx;
    private float rotationy;
    private AudioManager AU;
    private TutorialChanger TC;

    public DocumentManager DocMan;

    private Player Player;
    private Item item;

    [SerializeField]
    InventoryDocument DocumentInventory;
    [SerializeField]
    DiaryManager Diary;
    [SerializeField]
    DocumentObject Document;

    private void Start()
    {
        if (TryGetComponent(out TutorialChanger t)) TC = t;
        PropertyType = transform.GetChild(0).tag;

        player = GameObject.Find("Main Camera").transform;
        AU = GameObject.Find("GameGlobeData").GetComponent<AudioManager>();
       
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
            AU.PullSound(this.transform.position, 2, AudioNum);
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
                if(!gameObject.GetComponent<Rigidbody>().isKinematic) gameObject.GetComponent<Rigidbody>().isKinematic = true;
                if (Input.GetKey(KeyCode.Q))
                {
                    if (!GameGlobeData.IsCamLock) GameGlobeData.IsCamLock = true;
                    if (Cursor.lockState != CursorLockMode.None)Cursor.lockState = CursorLockMode.None;
                    if (Cursor.visible) Cursor.visible = false;

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
                if (GameGlobeData.IsCamLock) GameGlobeData.IsCamLock = false;
                if (Cursor.lockState != CursorLockMode.Locked) Cursor.lockState = CursorLockMode.Locked;
            }

            if (Input.GetKeyUp(KeyCode.Q))
            {
                if (GameGlobeData.IsCamLock) GameGlobeData.IsCamLock = false;
                if (Cursor.lockState != CursorLockMode.Locked) Cursor.lockState = CursorLockMode.Locked;
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

            if (CollectableType == CollectableList[1]) //CollectableList[1] --- Weapon;
            {

                    if (item == null) item = gameObject.GetComponent<Item>();
                    if (item)
                    {
                        if (Player == null) Player = GameObject.Find("Player").GetComponent<Player>();
                        Player.inventory.AddItem(item.item, 1);
                        hasInteracted = false;
                        Destroy(gameObject);
                    }
            }
        }
        #endregion

        #region READABLE
        if(PropertyType == PropertyList[2]) //PropertyList[2] --- Readable;
        {
            if (TC != null) TC.ChangeT();
            GameGlobeData.IsDocumentCollected = true;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                GameGlobeData.IsDocumentCollected = false;
                hasInteracted = false;
            }

            if (DocumentIndex == 0)
            {
               var title = "Documento #1 Arrependimento";
               var body = "Eu sabia que essa ideia era maluca. Uma ferrovia que atravessa a Amazônia e o Acre? Isso é um absurdo, não me surpreende que levaram décadas para concretizar a ideia. Por que aceitei trabalhar nessa insanidade? " +
                    "Além do trabalho impossível e baixo salário, nossos acampamentos estão sendo atacados por onças. Várias pessoas estão tendo que fugir e se abrigar em tendas improvisadas. Porém, todos que se abrigarem nessas tendas devem voltar o mais rápido possível para o acampamento. Dizem que ficar sozinho na mata te corrompe. " +
                    "Estou cansado, minha cabeça está quente, tão quente que cheguei a enxergar chamas a noite. Quero ir para casa.";
               DocMan.ChangeDocument(title, body);

               AddDocumentToSO(DocumentInventory);
               Diary.addDocumentToList(LoadDocumentFromSO(DocumentIndex, DocumentInventory));

            }

            if (DocumentIndex == 1)
            {
                var title = "Documento #2 = Bolsonaro Genocida";
                var body = "É indiscutível, no cenário atual de toda a esfera sócio-politica brasileira, que a conduta e a postura do atual presidente da república, Jair Bolsonaro, são índices indiscútiveis de um GENOCIDA. Eu estou escrevendo isso de placeholder pro segundo documento, mas a verdade é que enquanto desenvolvo esse projeto, meu coração se enche duvidas e incertezas sobre o meu futuro e o futuro do país com um governante tão boçal, desprepado e ignorante. Não apenas o governante em si, mas ver uma considerável massa apoiadora de um chorume desses me faz cada vez pensar que não temos salvação. É isso amigos. Obrigado por ler até aqui.";
                DocMan.ChangeDocument(title, body);

                AddDocumentToSO(DocumentInventory);
                Diary.addDocumentToList(LoadDocumentFromSO(DocumentIndex, DocumentInventory));
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

    public void AddDocumentToSO(InventoryDocument Inventory)
    {
        Inventory.AddItem(Document, 1);          
    }

    public DocumentObject LoadDocumentFromSO(int index, InventoryDocument Inventory) 
    {
        var Document = Inventory.Container[index].item;
        return Document;
    }


    #endregion

}
