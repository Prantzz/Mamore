using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class objectController : MonoBehaviour
{
    //coloquei essa propriedade somente leitura aqui pra poder identificar objetos seguráveis que não são ferramentas
    //assim da pra impedir o jogador de colocar pés de mesas nos trilhos ou nos degraus etc.

    [SerializeField] private string Type;
    public string type 
    { 
        get
        {
            return Type;
        } 
    }

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
    public PhotoCon phoCon;
    public Sprite photoImg;

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
        PropertyList.Add("Photo");
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
                if (!gameObject.GetComponent<Rigidbody>().isKinematic) gameObject.GetComponent<Rigidbody>().isKinematic = true;
                if (Input.GetKey(KeyCode.Q))
                {
                    if (!GameGlobeData.IsCamLock) GameGlobeData.IsCamLock = true;
                    if (Cursor.lockState != CursorLockMode.None) Cursor.lockState = CursorLockMode.None;
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
            if (CollectableType == CollectableList[0]) //CollectableList[0] --- Compass;
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
        if (PropertyType == PropertyList[2]) //PropertyList[2] --- Readable;
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
                var title = "Documento #1 Arrependimento.";
                var body = "Eu sabia que essa ideia era maluca. Uma ferrovia que atravessa a Amazônia e o Acre? Isso é um absurdo, não me surpreende que levaram décadas para concretizar a ideia. Por que aceitei trabalhar nessa insanidade? " +
                     "Além do trabalho impossível e baixo salário, nossos acampamentos estão sendo atacados por onças. Várias pessoas estão tendo que fugir e se abrigar em tendas improvisadas. Porém, todos que se abrigarem nessas tendas devem voltar o mais rápido possível para o acampamento. Dizem que ficar sozinho na mata te corrompe. " +
                     "Estou cansado, minha cabeça está quente, tão quente que cheguei a enxergar chamas a noite. Quero ir para casa.";
                DocMan.ChangeDocument(title, body);

                AddDocumentToSO(DocumentInventory);
                Diary.addDocumentToList(LoadDocumentFromSO(DocumentIndex, DocumentInventory));

            }

            if (DocumentIndex == 1)
            {
                var title = "Documento #2 = Tarde demais.";
                var body = "Fomos atacados por uma onça de novo, dessa vez mal consegui escapar. Estou começando a ficar com medo dos rumores serem verdadeiros, ouvi alguns barulhos estranhos na mata enquanto estava correndo. Acho que só eu e o fotógrafo carioca conseguimos fugir, consegui avistar ele montando uma barraca de frente à fogueira. Tinha esperanças de encontrar um carrinho de minérios por aqui, e sair logo dessa maldita floresta... mas agora já é tarde demais para mim.";
                DocMan.ChangeDocument(title, body);

                AddDocumentToSO(DocumentInventory);
                Diary.addDocumentToList(LoadDocumentFromSO(DocumentIndex, DocumentInventory));
            }

            if (DocumentIndex == 2)
            {
                var title = "Documento #3 = Detalhes.";
                var body = "Essa parte da ferrovia anda complicada, uns trilhos estão faltando e as alavancas estão mal posicionadas. Meus homens sempre estão sumindo, não sei o que está acontecendo com o acampamento lá atrás, esses dias até vi uns homens correndo de lá pra cá. De qualquer forma irei descobrir em breve, o carrinho de testes que leva até lá está funcionando, só preciso ajustar esses detalhes.";
                DocMan.ChangeDocument(title, body);

                AddDocumentToSO(DocumentInventory);
                Diary.addDocumentToList(LoadDocumentFromSO(DocumentIndex, DocumentInventory));
            }
            if (DocumentIndex == 3)
            {
                var title = "Documento #4 = É Real.";
                var body = "É real! A criatura é real! Eu vi a chama me perseguindo, parecia querer me expulsar daqui a todo custo. Tentei o revidar com flechas mas foi inútil, pelo menos este arco me serviu para coletar alguns alimentos. Aquela luz forte me deu náuseas… Que Deus me proteja dessa maldição.";
                DocMan.ChangeDocument(title, body);

                AddDocumentToSO(DocumentInventory);
                Diary.addDocumentToList(LoadDocumentFromSO(DocumentIndex, DocumentInventory));
            }
            if (DocumentIndex == 4)
            {
                var title = "Documento #5 = Sr. Buster.";
                var body = "Já é o segundo mês que faltam equipamentos de construção, os trilhos estão pela metade, e quando os americanos vêem, só sabem colocar a culpa na gente. Sem falar da falta de alimentos, vejo homens morrendo por malária e outros por falta de comida. Até enviaram uns doutores aqui, um tal de O.C e um B.P. mas não adianta nada sem os equipamentos. E pra piorar tudo meu M. M.Buster fugiu, ele se assustou com uma claridade e saiu correndo pela mata. Ele era meu único amigo aqui. Sr.Buster, Kelly sente dos seus latidos, onde está você amiguinho?";
                DocMan.ChangeDocument(title, body);

                AddDocumentToSO(DocumentInventory);
                Diary.addDocumentToList(LoadDocumentFromSO(DocumentIndex, DocumentInventory));
            }
            if (DocumentIndex == 5)
            {
                var title = "Documento #6 = -- DOCUMENTO OFICIAL QHUARFAR® --";
                var body = "Resposta ao pedido de revisão de trabalho e entrega de recursos. PARA DOUTOR O.C. Foi descoberta uma nova fonte abundante de látex no oriente, os preços estão tão baratos que a coleta de látex na América Latina já não adianta de mais nada. O transporte desse produto é o motivo dessa ferrovia ser construída, doutor, você entende o que quero dizer? Sei que estão passando dificuldades, mas não seria a primeira vez, não é? Peço paciência, infelizmente a prioridade agora é outra. Favor descartar este documento imediatamente. Dir.Qhuarfar.";
                DocMan.ChangeDocument(title, body);

                AddDocumentToSO(DocumentInventory);
                Diary.addDocumentToList(LoadDocumentFromSO(DocumentIndex, DocumentInventory));
            }

        }
        #endregion
        #region PHOTO
        if(PropertyType == PropertyList[3])  //PropertyList[3] --- Photo;
        {
            if (TC != null) TC.ChangeT();
            GameGlobeData.IsPhotoCollected = true;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                GameGlobeData.IsPhotoCollected = false;
                hasInteracted = false;
            }
            phoCon.ChangePhoto(photoImg);
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
