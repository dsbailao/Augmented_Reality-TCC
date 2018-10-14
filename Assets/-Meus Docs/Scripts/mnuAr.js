#pragma strict
	var mnuPrincipal : GameObject;
	var mnuSecundario : GameObject;

function Start() {
	mnuPrincipal.gameObject.SetActive(true);
	mnuSecundario.gameObject.SetActive(false);
}


function AbrirOpcoes() {
	mnuPrincipal.gameObject.SetActive(false);
	mnuSecundario.gameObject.SetActive(true);
}
function FecharOpcoes() {
	mnuPrincipal.gameObject.SetActive(true);
	mnuSecundario.gameObject.SetActive(false);
}

function btnMnuPrincipal() {
	Application.LoadLevel("MenuInicial");
}