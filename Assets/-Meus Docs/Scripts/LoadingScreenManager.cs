using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour {

	[Header("Visual do Menu")]
	public Image iconeCarregando;
	public Image iconeCarregandoFeito;
	public Text textoCarregando;
	public Image barraProgresso;
	public Image fadeOverlay;

	[Header("Configurações de Tempo")]
	public float esperaTerminoCarregando = 0.25f;
	public float duracaoFade = 0.25f;

	[Header("Configurações de Carregando")]
	public LoadSceneMode loadSceneMode = LoadSceneMode.Single;
	public ThreadPriority loadThreadPriority;

	[Header("Outros")]
	public AudioListener audioListener;

	AsyncOperation operation;
	Scene currentScene;

	public static int sceneToLoad = -1;
	// IMPORTANTE! Esse é o index da scene de carregando. Alterar caso mude INDEX!
	static int loadingSceneIndex = 2;

	public static void LoadScene(int sceneEnum) {				
		Application.backgroundLoadingPriority = ThreadPriority.High;
		sceneToLoad = sceneEnum;
		SceneManager.LoadScene(loadingSceneIndex);
	}

	void Start() {
		if (sceneToLoad < 0)
			return;

		fadeOverlay.gameObject.SetActive(true);
		currentScene = SceneManager.GetActiveScene();
		StartCoroutine(LoadAsync(sceneToLoad));
	}

	private IEnumerator LoadAsync(int levelNum) {
		ShowLoadingVisuals();

		yield return null; 

		FadeIn();
		StartOperation(levelNum);

		float lastProgress = 0f;

		// operation does not auto-activate scene, so it's stuck at 0.9
		while (DoneLoading() == false) {
			yield return null;

			if (Mathf.Approximately(operation.progress, lastProgress) == false) {
				barraProgresso.fillAmount = operation.progress;
				lastProgress = operation.progress;
			}
		}

		if (loadSceneMode == LoadSceneMode.Additive)
			audioListener.enabled = false;

		ShowCompletionVisuals();

		yield return new WaitForSeconds(esperaTerminoCarregando);

		FadeOut();

		yield return new WaitForSeconds(duracaoFade);

		if (loadSceneMode == LoadSceneMode.Additive)
			SceneManager.UnloadScene(currentScene.name);
		else
			operation.allowSceneActivation = true;
	}

	private void StartOperation(int levelNum) {
		Application.backgroundLoadingPriority = loadThreadPriority;
		operation = SceneManager.LoadSceneAsync(levelNum, loadSceneMode);


		if (loadSceneMode == LoadSceneMode.Single)
			operation.allowSceneActivation = false;
	}

	private bool DoneLoading() {
		return (loadSceneMode == LoadSceneMode.Additive && operation.isDone) || (loadSceneMode == LoadSceneMode.Single && operation.progress >= 0.9f); 
	}

	void FadeIn() {
		fadeOverlay.CrossFadeAlpha(0, duracaoFade, true);
	}

	void FadeOut() {
		fadeOverlay.CrossFadeAlpha(1, duracaoFade, true);
	}

	void ShowLoadingVisuals() {
		iconeCarregando.gameObject.SetActive(true);
		iconeCarregandoFeito.gameObject.SetActive(false);

		barraProgresso.fillAmount = 0f;
		textoCarregando.text = "CARREGANDO...";
	}

	void ShowCompletionVisuals() {
		iconeCarregando.gameObject.SetActive(false);
		iconeCarregandoFeito.gameObject.SetActive(true);

		barraProgresso.fillAmount = 1f;
		textoCarregando.text = "CARREGANDO COMPLETO";
	}

}