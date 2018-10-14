using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CarregarScenes : MonoBehaviour {

	public AudioSource somBotao;

	public void CarregarMenu (int idMenu) {
		if (idMenu < 0 || idMenu >= SceneManager.sceneCountInBuildSettings) {
			Debug.LogWarning("Não foi possivel encontrar a cena" + idMenu);
			return;
		}

		somBotao.Play();
		LoadingScreenManager.LoadScene(idMenu);

		//if (Application.platform != RuntimePlatform.Android) return;

		//Handheld.Vibrate();
	}
}
