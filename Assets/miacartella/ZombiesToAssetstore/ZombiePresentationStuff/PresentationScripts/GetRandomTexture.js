var textures: Texture[];
var rndNr:float;

function Awake () {
rndNr=Mathf.Ceil(Random.value*textures.length);
rndNr-=1;

renderer.material.mainTexture=textures[rndNr];
}

function Update () {
}