#pragma strict

public var speed : float = 3.0f;
public var jumpPower : float = 6.0f;

private var direction : Vector3;
private var controller : CharacterController;
private var anim : Animator;

function Start () {
controller = GetComponent.<CharacterController>();
anim = GetComponent.<Animator>();
}

function Update () {

if(controller.isGrounded){
var inputX : float = Input.GetAxis("Horizontal");
var inputY : float = Input.GetAxis("Vertical");
var inputDirection : Vector3 = Vector3(inputX,0,inputY);
direction = Vector3.zero;

if(inputDirection.magnitude > 0.1f){
transform.LookAt(transform.position + inputDirection);
direction += transform.forward * speed;
anim.SetFloat("Speed",direction.magnitude);
}else{
anim.SetFloat("Speed",0);
}

if(Input.GetButton("Jump")){
anim.SetTrigger("Jump");
direction.y += jumpPower;
}
}

controller.Move(direction * Time.deltaTime);
direction.y += Physics.gravity.y * Time.deltaTime;
}