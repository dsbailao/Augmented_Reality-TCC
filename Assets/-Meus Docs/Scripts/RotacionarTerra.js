#pragma strict

	public var velocidadeRotacao : int;

function Start () {

}

function Update () {
	transform.Rotate(0, -Time.deltaTime*velocidadeRotacao, 0);
}