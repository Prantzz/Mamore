using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class objectController : MonoBehaviour
{
    //coloquei essa propriedade somente leitura aqui pra poder identificar objetos segur�veis que n�o s�o ferramentas
    //assim da pra impedir o jogador de colocar p�s de mesas nos trilhos ou nos degraus etc.

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
    public event EventHandler OnInteraction; //usando esse pro puzzle2
    [SerializeField] private float zeroY;
    public List<String> PropertyList;
    public List<String> CollectableList;
    public List<String> ReadableList;
    public int AudioNum;

    private bool isDissolvind = false;
    private MeshRenderer meshRenderer;
    private float dissolveTime = 1;
    private float timer = -1;

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
    private Puzzle puzzle;

    [SerializeField]
    InventoryDocument DocumentInventory;
    [SerializeField]
    DiaryManager Diary;
    [SerializeField]
    DocumentObject Document;


    public Rigidbody objectRB;
    public bool shouldRotate = false;
    public bool objIsGrabbed = false;
    public bool isHoldable = false;
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if (TryGetComponent(out TutorialChanger t)) TC = t;
        PropertyType = transform.GetChild(0).tag;
        objectRB = gameObject.GetComponent<Rigidbody>();
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
        if (isSelected)
        {
            Selection();
        }

        if (hasInteracted)
        {
            Interactions();
        }
        if (transform.position.y <= zeroY) KeepObjectAboveGround();
    }

    //dissolver objeto dps que pegou e desativar
    private IEnumerator Dissolve()
    {
        while (true)
        {
            timer += dissolveTime * Time.deltaTime;
            if (timer < 1.5f)
            {
                meshRenderer.material.SetFloat("_Dissolve", timer);
            }    
            else
            {
                gameObject.SetActive(false);
                break;
            }
            yield return null;
        }
        yield return null;
    }

    #region UPDATE_METHODS
    private void KeepObjectAboveGround()
    {
        transform.position = new Vector3(transform.position.x, zeroY, transform.position.z);
    }

    public void Selection()
    {
        if (Input.GetKeyDown(KeyCode.E) && !hasInteracted)
        {
            AU.PullSound(this.transform.position, 2, AudioNum);
            hasInteracted = true;
        }
    }

    bool dirtyFlag = false;
    public void Interactions()
    {
        #region HOLDABLE
        if (isHoldable)
        {

            if (hasInteracted) //Input.GetKeyDown(KeyCode.E)
            {
                OnInteraction?.Invoke(this, EventArgs.Empty);
                if (transform.parent == null) transform.parent = player.transform;
                if (!objectRB.isKinematic) objectRB.isKinematic = true;
                if (!shouldRotate) shouldRotate = true;
                if (!objIsGrabbed) objIsGrabbed = true;

                if (type == "Mesa" && !dirtyFlag) // desajusta as pernas da mesa se o cara mexer na mesa sem martelar as pernas
                {
                    puzzle = GetComponentInChildren<Puzzle2>();
                    if (puzzle)
                        puzzle.DesajustarParte();

                    dirtyFlag = true;
                }
            }
            if (shouldRotate)
            {
                if (Input.GetKey(KeyCode.Q))
                {
                    if (!GameGlobeData.IsCamLock) GameGlobeData.IsCamLock = true;
                    if (Cursor.lockState != CursorLockMode.None) Cursor.lockState = CursorLockMode.None;
                    if (Cursor.visible) Cursor.visible = false;
                    
                    /* anterior
                    rotationx -= mPosY;
                    rotationy -= mPosX;
                    UpdateMouseValues();
                    //transform.Rotate(Vector3.up * mPosX);
                    //transform.Rotate(Vector3.right * mPosY);
                    transform.localRotation = Quaternion.Euler(-rotationx, rotationy, 0);
                    */

                    UpdateMouseValues();

                    //mudei aqui pra rotacionar em torno dos eixos locais do player (c�mera)
                    //dessa maneira pra cima sempre ser� o meu pra cima, pra baixo sempre ser� o meu pra baixo e assim por diante

                    //gon'take over the world while these haters gettin' mad
                    transform.Rotate(player.up, -mPosX, Space.World);
                    transform.Rotate(player.right, mPosY, Space.World);
                }
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                dirtyFlag = false;
                gameObject.transform.parent = null;
                objectRB.isKinematic = false;
                hasInteracted = false;
                objIsGrabbed = true;
                shouldRotate = false;
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
        if (!objIsGrabbed)
        {
            if (PropertyType == PropertyList[0]) //PropertyList[0] --- Holdable;
            {
                isHoldable = true;
            }
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
                    if (!isDissolvind)
                    {
                        tag = "Untagged";
                        isDissolvind = true;
                        StartCoroutine(Dissolve());
                    }
                }

                if (DocumentIndex == 0)
                {
                    var title = "Documento #1 Arrependimento.";
                    var body = "Eu sabia que essa ideia era maluca. Uma ferrovia que atravessa a Amaz�nia e o Acre? Isso � um absurdo, n�o me surpreende que levaram d�cadas para concretizar a ideia. Por que aceitei trabalhar nessa insanidade? " +
                         "Al�m do trabalho imposs�vel e baixo sal�rio, nossos acampamentos est�o sendo atacados por on�as. V�rias pessoas est�o tendo que fugir e se abrigar em tendas improvisadas. Por�m, todos que se abrigarem nessas tendas devem voltar o mais r�pido poss�vel para o acampamento. Dizem que ficar sozinho na mata te corrompe. " +
                         "Estou cansado, minha cabe�a est� quente, t�o quente que cheguei a enxergar chamas a noite. Quero ir para casa.";
                    DocMan.ChangeDocument(title, body);

                    AddDocumentToSO(DocumentInventory);
                    Diary.addDocumentToList(LoadDocumentFromSO(DocumentIndex, DocumentInventory));

                }

                if (DocumentIndex == 1)
                {
                    var title = "Documento #2 = Tarde demais.";
                    var body = "Fomos atacados por uma on�a de novo, dessa vez mal consegui escapar. Estou come�ando a ficar com medo dos rumores serem verdadeiros, ouvi alguns barulhos estranhos na mata enquanto estava correndo. Acho que s� eu e o fot�grafo carioca conseguimos fugir, consegui avistar ele montando uma barraca de frente � fogueira. Tinha esperan�as de encontrar um carrinho de min�rios por aqui, e sair logo dessa maldita floresta... mas agora j� � tarde demais para mim.";
                    DocMan.ChangeDocument(title, body);

                    AddDocumentToSO(DocumentInventory);
                    Diary.addDocumentToList(LoadDocumentFromSO(DocumentIndex, DocumentInventory));
                }

                if (DocumentIndex == 2)
                {
                    var title = "Documento #3 = Detalhes.";
                    var body = "Essa parte da ferrovia anda complicada, uns trilhos est�o faltando e as alavancas est�o mal posicionadas. Meus homens sempre est�o sumindo, n�o sei o que est� acontecendo com o acampamento l� atr�s, esses dias at� vi uns homens correndo de l� pra c�. De qualquer forma irei descobrir em breve, o carrinho de testes que leva at� l� est� funcionando, s� preciso ajustar esses detalhes.";
                    DocMan.ChangeDocument(title, body);

                    AddDocumentToSO(DocumentInventory);
                    Diary.addDocumentToList(LoadDocumentFromSO(DocumentIndex, DocumentInventory));
                }
                if (DocumentIndex == 3)
                {
                    var title = "Documento #4 = � Real.";
                    var body = "� real! A criatura � real! Eu vi a chama me perseguindo, parecia querer me expulsar daqui a todo custo. Tentei o revidar com flechas mas foi in�til, pelo menos este arco me serviu para coletar alguns alimentos. Aquela luz forte me deu n�useas� Que Deus me proteja dessa maldi��o.";
                    DocMan.ChangeDocument(title, body);

                    AddDocumentToSO(DocumentInventory);
                    Diary.addDocumentToList(LoadDocumentFromSO(DocumentIndex, DocumentInventory));
                }
                if (DocumentIndex == 4)
                {
                    var title = "Documento #5 = Sr. Buster.";
                    var body = "J� � o segundo m�s que faltam equipamentos de constru��o, os trilhos est�o pela metade, e quando os americanos v�em, s� sabem colocar a culpa na gente. Sem falar da falta de alimentos, vejo homens morrendo por mal�ria e outros por falta de comida. At� enviaram uns doutores aqui, um tal de O.C e um B.P. mas n�o adianta nada sem os equipamentos. E pra piorar tudo meu M. M.Buster fugiu, ele se assustou com uma claridade e saiu correndo pela mata. Ele era meu �nico amigo aqui. Sr.Buster, Kelly sente dos seus latidos, onde est� voc� amiguinho?";
                    DocMan.ChangeDocument(title, body);

                    AddDocumentToSO(DocumentInventory);
                    Diary.addDocumentToList(LoadDocumentFromSO(DocumentIndex, DocumentInventory));
                }
                if (DocumentIndex == 5)
                {
                    var title = "Documento #6 = -- DOCUMENTO OFICIAL QHUARFAR� --";
                    var body = "Resposta ao pedido de revis�o de trabalho e entrega de recursos. PARA DOUTOR O.C. Foi descoberta uma nova fonte abundante de l�tex no oriente, os pre�os est�o t�o baratos que a coleta de l�tex na Am�rica Latina j� n�o adianta de mais nada. O transporte desse produto � o motivo dessa ferrovia ser constru�da, doutor, voc� entende o que quero dizer? Sei que est�o passando dificuldades, mas n�o seria a primeira vez, n�o �? Pe�o paci�ncia, infelizmente a prioridade agora � outra. Favor descartar este documento imediatamente. Dir.Qhuarfar.";
                    DocMan.ChangeDocument(title, body);

                    AddDocumentToSO(DocumentInventory);
                    Diary.addDocumentToList(LoadDocumentFromSO(DocumentIndex, DocumentInventory));
                }
            }

        }
        #endregion
        #region PHOTO
        if (PropertyType == PropertyList[3])  //PropertyList[3] --- Photo;
        {
            if (TC != null) TC.ChangeT();

            GameGlobeData.IsPhotoCollected = true;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                GameGlobeData.IsPhotoCollected = false;
                hasInteracted = false;
                if (!isDissolvind)
                {
                    tag = "Untagged";
                    isDissolvind = true;
                    StartCoroutine(Dissolve());
                }

            }
            phoCon.ChangePhoto(photoImg);

        }
        #endregion
    }
    private void UpdateMouseValues()
    {
        mPosX = Input.GetAxis("Mouse X") * 10f * Time.deltaTime; //aumentei aqui a sensibilidade, n�o sei ficou muito mais lenta que antes
        mPosY = Input.GetAxis("Mouse Y") * 10f * Time.deltaTime;
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

