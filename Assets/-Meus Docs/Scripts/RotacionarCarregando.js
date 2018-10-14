#pragma strict

function Start () {

}

function Update () {
	transform.Rotate(0, 0, -Time.deltaTime*120);
}