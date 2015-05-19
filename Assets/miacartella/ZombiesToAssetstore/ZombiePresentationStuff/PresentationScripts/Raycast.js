var moveThis : GameObject;
var hit : RaycastHit;
var createThis : GameObject[];
var explosion:GameObject;

var cooldown : float;
var changeCooldown : float;
var selected:int=0;
//var writeThis:GUIText;

function Start () {
//writeThis.text=selected.ToString();
}

function Update () {
if(cooldown>0){cooldown-=Time.deltaTime;}
if(changeCooldown>0){changeCooldown-=Time.deltaTime;}

var ray = Camera.main.ScreenPointToRay (Input.mousePosition);

if (Physics.Raycast (ray, hit)) {
// Create a particle if hit
moveThis.transform.position=hit.point;

if(Input.GetMouseButton(0)&&cooldown<=0){

Instantiate(createThis[0], moveThis.transform.position, moveThis.transform.rotation);
Instantiate(explosion, moveThis.transform.position, moveThis.transform.rotation);


cooldown=0.1;
}



//Instantiate (particle, hit.point, transform.rotation);

}







}