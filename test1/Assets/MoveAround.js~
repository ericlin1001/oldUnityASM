#pragma strict
var speed=3.0;
var rotateSpeed=3.0;


function Update () {
	var controller:CharacterController=GetComponent(CharacterController);
	//
	transform.Rotate(0,Input.GetAxis("Horizontal")*rotateSpeed,0);

	//move forward,backward.
	var forward=transform.TransformDirection(Vector3.forward);
	var curSpeed=speed*Input.GetAxis("Vertical");
	controller.SimpleMove(forward*curSpeed);

	
}
@script RequireComponent(CharacterController);
