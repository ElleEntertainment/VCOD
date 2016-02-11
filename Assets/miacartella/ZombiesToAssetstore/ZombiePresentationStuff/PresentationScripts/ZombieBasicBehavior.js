var zombieState:int;  // 0 - idle áll 1 - shambe 
var newZombieState:int;
var stateChangerCD:float;
var rndNr:float;
var myBoxCollider:BoxCollider;
var blood:GameObject;
var HP:int=3;
var HPCD:int;

var Zombie:GameObject;
var createThis:GameObject[];

function Start () {
stateChangerCD=3.0;
zombieState=2;
rndNr=Mathf.Ceil(Random.value*createThis.length);
rndNr-=1;
Zombie=Instantiate(createThis[rndNr], transform.position, transform.rotation);
Zombie.transform.parent=transform;
}

function Update () {
if(HPCD>0) HPCD-=Time.deltaTime;   //HPCD: hit point cooldown


stateChangerCD-=Time.deltaTime;

//************************* DONTES
if(stateChangerCD<0)
		{
		stateChangerCD=1.5+Random.value;
		
		if(zombieState==0){ //ha idlézett
		rndNr=Random.value*100;
		if(rndNr<20){newZombieState=0;} //marad idle
		if((rndNr>=20)&&(rndNr<=30)){newZombieState=1;} //shambling
		if((rndNr>30)&&(rndNr<=60)){newZombieState=2;} //walking
		if((rndNr>60)&&(rndNr<=100)){newZombieState=12;} //running
		}
				
		if(zombieState==1){  //shamble
		rndNr=Random.value*100;
		if(rndNr<20){newZombieState=0;} //idle
		if((rndNr>=20)&&(rndNr<=75)){newZombieState=1;} //shambling
		if((rndNr>80)&&(rndNr<=90)){newZombieState=2;} //walking
		if((rndNr>90)&&(rndNr<=100)){newZombieState=3;} //fallToFace
		if((rndNr>75)&&(rndNr<=80)){newZombieState=12;} //running
		}
		
		if(zombieState==12){   //run
		rndNr=Random.value*100;
		if((rndNr>0)&&(rndNr<=20)){newZombieState=2;} //walking
		if((rndNr>20)&&(rndNr<=75)){newZombieState=12;} //running
		if((rndNr>75)&&(rndNr<=100)){newZombieState=3;		stateChangerCD=0.5;} //fallToFace
		}
		
		if(zombieState==2){   //walk
		rndNr=Random.value*100;
		if(rndNr<20){newZombieState=0;} //idle
		if((rndNr>=20)&&(rndNr<=60)){newZombieState=1;} //shambling
		if((rndNr>60)&&(rndNr<=80)){newZombieState=2;} //walking
		if((rndNr>80)&&(rndNr<=90)){newZombieState=12;} //running
		if((rndNr>90)&&(rndNr<=100)){newZombieState=3;		stateChangerCD=0.5;} //fallToFace
		}

		if(zombieState==3){ //falltoface
		newZombieState=4;
		stateChangerCD=0.5;
		}
		
		if(zombieState==4){ //begintocrawl
		rndNr=Random.value*100;
		if((rndNr>=0)&&(rndNr<=50)){newZombieState=5;} //crawl
		if((rndNr>50)&&(rndNr<=100)){newZombieState=6;stateChangerCD=1;} //standUp
		}
		
		if(zombieState==5){ //crawl
		rndNr=Random.value*100;
		if((rndNr>=0)&&(rndNr<=90)){newZombieState=5;} //crawl
		if((rndNr>90)&&(rndNr<=100)){newZombieState=6;		stateChangerCD=1;} //standUp
		}
		
		if(zombieState==6){  //standup
		newZombieState=0; 
				}
		
			if(zombieState==7){  //fallBack
		newZombieState=7;
myBoxCollider.size=Vector3(0,0,0);
Destroy(gameObject, 1.3);		
		stateChangerCD=10;
			}

		if(zombieState==8){  //incured
		newZombieState=0; 
				}
				
		if(zombieState==9){  //incured
		newZombieState=0; 
				}


		if(zombieState==10||zombieState==11){  //attack
		newZombieState=Mathf.Ceil(Random.value*2)+9;  //10 vagy 11
		stateChangerCD=0.5;
				}



	
		zombieState=newZombieState;
		
		}


//*************** Dontes vege



if(zombieState==0)
	{
	if (!Zombie.GetComponent.<Animation>().IsPlaying("idle"))
		{
		Zombie.GetComponent.<Animation>().CrossFade("idle", 0.3);
		}
	
	}


if(zombieState==1)
	{
	transform.Translate(0,0,1*Time.deltaTime);

	
	if (!Zombie.GetComponent.<Animation>().IsPlaying("shamble"))
		{
		Zombie.GetComponent.<Animation>().CrossFade("shamble", 0.3);
		}
	
	}

if(zombieState==2)
	{
	transform.Translate(0,0,1*Time.deltaTime);
	
	if (!Zombie.GetComponent.<Animation>().IsPlaying("walk"))
		{
		Zombie.GetComponent.<Animation>().CrossFade("walk", 0.3);
		}
	
	}


if(zombieState==3)
	{
		
	if (!Zombie.GetComponent.<Animation>().IsPlaying("fallToFace"))
		{
		Zombie.GetComponent.<Animation>().CrossFade("fallToFace", 0.1);
		}
	
	}

if(zombieState==4)
	{
		
	if (!Zombie.GetComponent.<Animation>().IsPlaying("beginToCrawl"))
		{
		Zombie.GetComponent.<Animation>().CrossFade("beginToCrawl", 0.1);
		}
	
	}

if(zombieState==5)
	{
	transform.Translate(0,0,1*Time.deltaTime);
	
	if (!Zombie.GetComponent.<Animation>().IsPlaying("crawl"))
		{
		Zombie.GetComponent.<Animation>().CrossFade("crawl", 0.1);
		}
	
	}
	
	
if(zombieState==6)
	{
	
	
	if (!Zombie.GetComponent.<Animation>().IsPlaying("standUp"))
		{
		Zombie.GetComponent.<Animation>().CrossFade("standUp", 0.1);
		}
	
	}


if(zombieState==7)
	{
	
	
	if (!Zombie.GetComponent.<Animation>().IsPlaying("fallBack"))
		{
		Zombie.GetComponent.<Animation>().CrossFade("fallBack", 0.1);
		}
	
	}
	
	if(zombieState==8)
	{

	if (!Zombie.GetComponent.<Animation>().IsPlaying("hit1"))
		{
		Zombie.GetComponent.<Animation>().CrossFade("hit1", 0.1);
		}
	}
	
	if(zombieState==9)
	{

	if (!Zombie.GetComponent.<Animation>().IsPlaying("hit2"))
		{
		Zombie.GetComponent.<Animation>().CrossFade("hit2", 0.1);
		}
	}


	if(zombieState==10)
	{

	if (!Zombie.GetComponent.<Animation>().IsPlaying("attack1"))
		{
		Zombie.GetComponent.<Animation>().CrossFade("attack1", 0.1);
		}
	}

	if(zombieState==11)
	{

	if (!Zombie.GetComponent.<Animation>().IsPlaying("attack2"))
		{
		Zombie.GetComponent.<Animation>().CrossFade("attack2", 0.1);
		}
	}

if(zombieState==12)
	{
	transform.Translate(0,0,3*Time.deltaTime);
	
	if (!Zombie.GetComponent.<Animation>().IsPlaying("run"))
		{
		Zombie.GetComponent.<Animation>().CrossFade("run", 0.3);
		}
	
	}

}


function OnTriggerEnter(other : Collider) {

if(other.tag=="barrier"&&zombieState!=10)
	{
		Debug.Log("Barrier found");
		zombieState=10;
		stateChangerCD=0.01;
	}



if(other.tag=="explo")
{
Instantiate(blood, transform.position, transform.rotation);
	if(HP==1)
	{
		zombieState=7;
		stateChangerCD=0.01;
	}
	
	if(HP>1&&HPCD<=0)
	{	// akkor valamelyik serules jon
		stateChangerCD=0.7;
		zombieState=7+Mathf.Ceil(Random.value*2);  //8 or 9
		HPCD=0.7;
		HP-=1;
	}
}




}
