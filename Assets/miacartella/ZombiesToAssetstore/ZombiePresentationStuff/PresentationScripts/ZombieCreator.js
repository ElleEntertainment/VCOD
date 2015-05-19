var CD:float=1.0;
var createThis:GameObject[];
var rndNr:float;

function Update () {

CD-=Time.deltaTime;

if(CD<0){
rndNr=Mathf.Ceil(Random.value*createThis.length);
rndNr-=1;
Instantiate(createThis[rndNr], Vector3((transform.position.x-5+(Random.value*10)), transform.position.y, transform.position.z), transform.rotation);

CD=0.2;
}


if (Input.GetKeyDown ("space")){
rndNr=Mathf.Ceil(Random.value*createThis.length);
rndNr-=1;
Instantiate(createThis[rndNr], Vector3((transform.position.x-5+(Random.value*10)), transform.position.y, transform.position.z+(Random.value*13)), transform.rotation);
Instantiate(createThis[rndNr], Vector3((transform.position.x-5+(Random.value*10)), transform.position.y, transform.position.z+(Random.value*13)), transform.rotation);
Instantiate(createThis[rndNr], Vector3((transform.position.x-5+(Random.value*10)), transform.position.y, transform.position.z+(Random.value*13)), transform.rotation);

Instantiate(createThis[rndNr], Vector3((transform.position.x-5+(Random.value*10)), transform.position.y, transform.position.z+(Random.value*13)), transform.rotation);
Instantiate(createThis[rndNr], Vector3((transform.position.x-5+(Random.value*10)), transform.position.y, transform.position.z+(Random.value*13)), transform.rotation);
Instantiate(createThis[rndNr], Vector3((transform.position.x-5+(Random.value*10)), transform.position.y, transform.position.z+(Random.value*13)), transform.rotation);

Instantiate(createThis[rndNr], Vector3((transform.position.x-5+(Random.value*10)), transform.position.y, transform.position.z+(Random.value*13)), transform.rotation);
Instantiate(createThis[rndNr], Vector3((transform.position.x-5+(Random.value*10)), transform.position.y, transform.position.z+(Random.value*13)), transform.rotation);
Instantiate(createThis[rndNr], Vector3((transform.position.x-5+(Random.value*10)), transform.position.y, transform.position.z+(Random.value*13)), transform.rotation);

}

}