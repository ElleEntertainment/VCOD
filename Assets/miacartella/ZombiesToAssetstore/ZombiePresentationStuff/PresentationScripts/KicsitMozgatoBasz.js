var curX:float;
var curZ:float;

function Start () {

curX=transform.position.x;
curZ=transform.position.z;

}

function Update () {
transform.Translate(0.01*Time.deltaTime, 0, 0.01*Time.deltaTime);
if((transform.position.x)>(curX+0.01)) 
{
transform.position.x=curX;
}


if((transform.position.z)>(curZ+0.007)) 
{
transform.position.z=curZ;
}


}