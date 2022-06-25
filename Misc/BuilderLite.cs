using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderLite : MonoBehaviour
{


    //Game manager is going to be a singleton.
    public static GameManager instance = null;


    [SerializeField]
    private LevelGenerator _levelGenerator;
    [SerializeField]
    private Transform _tracksContainer;
    [SerializeField]
    private SpeedHud _speedHud;

    [SerializeField]
    private Lights _lightsManager;

    [SerializeField]
    private ControllerInput _inputController;

    [SerializeField]
    private LevelOutroScript _levelOutro;
   
    [SerializeField]
    private CarMovement _carMovement;

    private List<Transform> _lights; 
    private List<GameObject>  _lightsRenderer;
    public int TrackSize;

    private CameraMovement _cameraMovement;

    public  FadeScreen _fadeScript;

    public Countdown _countdown;

    public Fireworks _fireworks;

    private GameObject _CameraLastPosition;

    public GameObject CameraLastPosition
	{
		get
		{
			return this._CameraLastPosition;
		}
		set
		{
			this._CameraLastPosition = value;
		}
	}


    void Start()
    {
       /*  if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);*/

        Setup();
    }


    void Setup()
    {
        // _currentTriangle = 0;
        _tracksContainer = GameObject.Find("StageTrack").transform;
        _levelGenerator = _tracksContainer.gameObject.GetComponent("LevelGenerator") as LevelGenerator;
      /*   _carMovement = GameObject.Find("Car").GetComponent("CarMovement") as CarMovement;
        _speedHud = GameObject.Find("HUDCanvas").GetComponent("SpeedHud") as  SpeedHud;
        _lightsManager =  GameObject.Find("Lights").GetComponent("Lights") as  Lights;
        _inputController = GameObject.Find("ControllerInput").GetComponent("ControllerInput") as ControllerInput;
        _cameraMovement = GameObject.Find("Car").transform.Find("Main Camera").GetComponent("CameraMovement") as CameraMovement;
        _fadeScript = GameObject.Find("Car").transform.Find("Main Camera").GetComponent("FadeScreen") as FadeScreen;
        _countdown = GameObject.Find("HUDCanvas").GetComponent("Countdown") as  Countdown;*/

        CreateLevel();
    }

    void CreateLevel(){
      //  _inputController.gameObject.SetActive(false);
      //  _inputController.ControllerVibration = 0f;
      //  _inputController.enabled = false;

      //  _fadeScript.fadeDir = 0;
      //  _fadeScript.alpha = 1f;
        _levelGenerator.BuildLevelProcedural(TrackSize);
      //  _lights = new List<Transform>();
      //  _lightsRenderer = new List<GameObject>();
      //  GetStageLights();
      //  _lightsManager.SetStreetLights(_lights);
      //  _fadeScript.fadeDir = -1f;
      //  _fadeScript.fadespeed = 0.2f;
      //  _countdown.DoCountDown(3);

    }

    public void SetStartFireworks(Fireworks fireworks){
        _fireworks = fireworks;
    }

    public void OnFadeComplete(){
        
    }

    public void OnCountDownComplete(){
        _inputController.gameObject.SetActive(true);
        _inputController.ControllerVibration = 0f;
        _inputController.enabled = true;
        _fireworks.EnableStartFireWorks();
    }

    public void FadeToBlack(){
        _fadeScript.fadeDir = 1f;
        _fadeScript.fadespeed = 0.2f;
    }



    void GetStageLights(){
        // Fetch each road part
        foreach (Transform part in _tracksContainer) {
            // For each road part, fetch the props including lights
            foreach (Transform prop in part) {
                if (prop.gameObject.name.StartsWith("LampPost")){
                    
                    _lights.Add(prop);
                    foreach (Transform element in prop) {
                        element.gameObject.SetActive( false);
                    }
                  //  Transform lightShape = prop.Find("LightShape");
                   // Debug.Log(lightShape);
                    //_lightsRenderer.Add( lightShape.gameObject);
                    //Debug.Log(lightShape);
                }
            }
        }
    }

    public void FinishGame(){
        _inputController.gameObject.SetActive(false);
        _inputController.ControllerVibration = 0f;
        _inputController.enabled = false;
        //_carMovement.enabled = false;
        _carMovement.PlayOutro = true;
        _cameraMovement.PlayOutro = true;
        _cameraMovement.OutroPosition =_CameraLastPosition;
    }



    // Update is called once per frame
    void Update()
    {
     /*    float carSpeed = _carMovement.GetCurrentSpeed();
        float maxSpeed = _carMovement.GetMaxSpeed();

        if (_inputController.gameObject.active){
           

            _carMovement.accelerationTrigger = _inputController.RightTrigger - _inputController.LeftTrigger;
            _carMovement.horizontalValue = _inputController.LeftStickX;
            _lightsManager.LightsChangeAction = _inputController.LeftBump;
            _cameraMovement.XAxis = _inputController.RightStickX;
            _cameraMovement.YAxis = _inputController.RightStickY;
            float speedModNormalized =  (_carMovement.GetCurrentSpeed() /_carMovement.GetMaxSpeed());
            _inputController.ControllerVibration =  speedModNormalized*0.1f + Mathf.Abs(_inputController.LeftStickX)* speedModNormalized * 0.2f; 
        }

        _speedHud.SetCurrentSpeed(Mathf.Abs(carSpeed));
        _speedHud.SetMaxSpeed(maxSpeed-1.05f);*/
       
    }


}
