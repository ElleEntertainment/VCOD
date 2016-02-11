var textures: Texture[];
var rndNr:float;

function Awake () {
rndNr=Mathf.Ceil(Random.value*textures.length);
rndNr-=1;

GetComponent.<Renderer>().material.mainTexture=textures[rndNr];
}

function Update () {
}