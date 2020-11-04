using UnityEngine;
using UnityEngine.SceneManagement; //para trabajar con cambios de escenas

//usamos namespaces para organizar mejor nuestro codigo. Respetamos la estructura que definimos aca en la carpeta de Scripts.
namespace Controllers
{
    public class SceneController : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private CanvasGroup canvasGroup = null; //componente que nos permite modificar propiedades generales de todos los elementos child de tipo UI.
        [SerializeField] private float transitionTime = 1.0f;
        #endregion

        #region PRIVATE_FIELDS
        private FloatLerper alphaLerper = null;
        private int nextScene = 0;
        #endregion

        #region ENUMS
        private enum STATE { IDLE, OPENING, CLOSING } // definimos estados para visualizar facilmente en que proceso se encuentra la transicion
        private STATE state = STATE.IDLE;

        public enum SCENES { MAINMENU, GAMEPLAY } //definimos enum de nombres de escenas para pasarlos como parametros
        #endregion

        //Singleton, revisar el pattern con la documentacion.
        //Nuestra clase tiene una variable estatica llamada instance, la cual depedende de la clase y no de objetos (no requerimos de esta manera
        //tener que crear referencias en cada codigo que llame a SceneController
        public static SceneController instance = null;

        #region UNITY_CALLS
        private void Awake()
        {
            Initialize();
        }

        private void Update()
        {
            UpdateTransition();
        }
        #endregion

        #region PUBLIC_METHODS
        public void LoadScene(int sceneIndex)
        {
            //Guardamos la escena proxima y abrimos la transicion
            nextScene = sceneIndex;
            OnTransitionStart();
        }

        public void LoadScene(SCENES scene)
        {
            //mismo caso, recibiendo como parametro un enum que casteamos a int por el build index que configuramos.
            nextScene = (int)scene;
            OnTransitionStart();
        }
        #endregion

        #region PRIVATE_METHODS
        private void Initialize()
        {
            //si no hay ninguna instancia
            if (instance == null)
            {
                //YO soy la instancia
                instance = this;
                //no destruyo mi gameobject para que perdure entre escenas, y asi funcione la transicion visualmente
                DontDestroyOnLoad(gameObject);
                alphaLerper = new FloatLerper(transitionTime, AbstractLerper<float>.SMOOTH_TYPE.STEP_SMOOTHER);
            }
            else
            {
                //sino me destruyo (porque otro es mi instance y no podemos tener 2)
                Destroy(gameObject);
            }
        }

        private void UpdateTransition()
        {
            if (!alphaLerper.On)
            {
                return;
            }

            alphaLerper.Update();
            canvasGroup.alpha = alphaLerper.CurrentValue;

            if (alphaLerper.Reached)
            {
                OnReached();
            }
        }

        private void OnReached()
        {
            switch (state)
            {
                case STATE.OPENING:
                    OnTransitionEnd();
                    break;
                case STATE.CLOSING:
                    OnTransitionAtMid();
                    break;
            }
        }

        private void OnTransitionStart()
        {
            //cuando arranca la transcion
            state = STATE.CLOSING;

            //tenemos que ir al alpha en 1, para poner todo negro
            alphaLerper.SetValues(canvasGroup.alpha, 1.0f, true);

            //bloqueamos la interaccion con la UI debajo de la UI de la transicion
            canvasGroup.blocksRaycasts = true;
        }

        private void OnTransitionAtMid()
        {
            //si el alpha llega a 1 estamos a la mitad
            state = STATE.OPENING;

            //aca hacemos el cambio real de la escena. Es un cambio sincronico, por lo que vamos a tener un mini cuelgue si la escena es pesada.
            SceneManager.LoadScene(nextScene);

            //ahora tenemos que ir devuelta a 0 alpha
            alphaLerper.SetValues(canvasGroup.alpha, 0.0f, true);
        }

        private void OnTransitionEnd()
        {
            //si el alpha esta en 0 termino la transicion, desbloqueamos la interaccion y ponemos el estado en idle para arrancar devuelta.
            canvasGroup.blocksRaycasts = false;
            state = STATE.IDLE;
        }
        #endregion
    }
}
