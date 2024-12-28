using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
using TapticPlugin;
public class FieldOfView : MonoBehaviour
{
    public float ViewRadius;
    [Range(0, 360)] public float ViewAngle;
    public LayerMask TargetMask;
    public LayerMask ObstacleMask;

    public bool DrawFieldOfView;
	public MeshFilter ViewMeshFilter;
    public float MeshResolution;
    public int EdgeResolveIterations;
	public float EdgeDstThreshold;

    public bool onTriggerValue = false;

    public int a;
    public Material targetColor;
    
    [NonSerialized] public Transform ClosestTarget;

    //private Soldier _soldier;
	private Mesh _viewMesh;

    private void Start() {
        _viewMesh = new Mesh ();
		_viewMesh.name = "View Mesh";
		ViewMeshFilter.mesh = _viewMesh;
        //GameManager.instance.enemyMaterial = ClosestTarget.GetComponent<Renderer>().material;
        //_soldier = GetComponent<Soldier>();
        
        StartCoroutine(FindTargetsWithDelay(0.2f));
    }

    private IEnumerator FindTargetsWithDelay(float delay) {
        while (true) {

            yield return new WaitForSeconds(delay);
            
            FindVisibleTargets();
        }
    }

    private void Update() {
        if(DrawFieldOfView) {
            DrawFOV();    
        }
    }


    private async void FindVisibleTargets() {
        ClosestTarget = null;
    //soldier.Rem    oveTarget();

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, ViewRadius, TargetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++) {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < ViewAngle / 2) {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, ObstacleMask)) {
                    if (ClosestTarget == null || dstToTarget < Vector3.Distance(transform.position, ClosestTarget.position)) {
                        //ClosestTarget düşman oluyor
                        ClosestTarget = target;
                        GameManager.instance.gunFire.Play();
                        
                        //ClosestTarget.transform.DOScale(0,2);
                        //ClosestTarget.transform.DOMove(GameManager.instance.player.transform.localPosition,1f);
                        if(GameObject.Find("EnemyBoss").transform.GetChild(0).gameObject.active == false){
                        if(SliderBarSystem.instance.currentBarValue <100 && ClosestTarget.transform.gameObject.GetComponent<EnemyMovement>().myHealth < PlayerController.instance.playerMyHealth){
                            SliderBarSystem.instance.NegativeUseMoney(-10);//Vurduğum zaman canım artıyor
                        }
                        }

                        //Karakterlerin hepsi düşmanı görünce ateş ediyor
                        /*
                        for(a = 0; a < 7; a++){
                            GameManager.instance.friendGunFire[a].Play();
                        }
                        */
                        //Karakterlerin hepsi düşmanı görünce ateş ediyor

                        //ClosestTarget.GetComponent<Animator>().SetBool("fail",true);
                        //ClosestTarget.GetComponent<EnemyFieldOfView>().enabled = false;

                        //ClosestTarget.transform.GetChild(2).gameObject.SetActive(false);

                        ClosestTarget.transform.LookAt(GameManager.instance.player.transform);//Vurduğum adam burada direk bana dönüyor

                        //PlayerController.instance.playerMyHealth ++;

                        if(GameObject.Find("EnemyBoss").transform.GetChild(0).gameObject.active == false){
                        if(PlayerController.instance.playerMyHealth + 1>=ClosestTarget.transform.GetComponent<EnemyMovement>().myHealth){
                            ClosestTarget.transform.GetComponent<EnemyMovement>().myHealth-= PlayerController.instance.playerMyHealth;
    
                        }
                        }
                        

                        //ClosestTarget.transform.GetComponent<BossMovement>().bossMyHeath--;
                       
                        Debug.Log("Düşman");

                        TapticPlugin.TapticManager.Impact(ImpactFeedback.Heavy);//Düşamnı gördüğünde büyük bir titreşim etkili olacak

                        //ClosestTarget.GetComponent<CapsuleCollider>().enabled = false;
                        //ClosestTarget.GetComponent<Renderer>().material.DOColor(Color.blue,1); 
                        //GameManager.instance.player.transform.DOPunchScale(new Vector3(GameManager.instance.player.transform.localScale.x,GameManager.instance.transform.localScale.y,GameManager.instance.transform.localScale.z),2);
                        //ClosestTarget.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.DOColor(GameManager.instance.friendMaterial.color,3);

                        //ClosestTarget.transform.DOScale(0,1).SetDelay(2).SetEase(Ease.Linear);//Vurduktan 2 saniye sonra küçülmeye başlıyor ve 1 saniyede küçülüyor

                        BossLevelSystem();
                        
                        if(GameObject.Find("EnemyBoss").transform.GetChild(0).gameObject.active == true){
                        if(BossMovement.instance.myFriendNumber == 0){
                            KillTheBoss();
                        }
                        }
                        
                        if(ClosestTarget.gameObject.layer != 0 && GameObject.Find("EnemyBoss").transform.GetChild(0).gameObject.active == false){
                        Debug.Log("değillll");
                        ClosestTargetColliderFalse();;//1 saniye sonra CapsuleColliderı devreden çıkaran fonksiyonu çağırdım
                        }
                        //ClosestTarget.gameObject.layer = 0;
                        //yield return new WaitForSeconds(1f);
                        //ClosestTarget.transform.DOScale(0,1);
                        //ClosestTarget.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.DOComplete();
                        //ClosestTarget.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = GameManager.instance.friendMaterial;
                        //ClosestTarget.transform.DOScale(0,1);
                        //ClosestTarget.transform.LookAt(GameManager.instance.player.transform);
                        //ClosestTarget.GetComponent<SkinnedMeshRenderer>().material.DOColor(Color.blue,1);
                        //_soldier.SetTarget(ClosestTarget);
                    }
                }
                
            }
        }

        if (ClosestTarget != null) {
            //RemoveFOV();
        }
    }

    public void KillTheBoss(){
        BossMovement.instance.bossMyHeath--;
        BossSliderBarSystem.instance.BossNegativeUseMoney(10);
        GameObject.Find("EnemyBoss").transform.GetChild(0).transform.localScale -= new Vector3(0.1f,0.1f,0.1f);
        GameObject.Find("EnemyBoss").transform.GetChild(0).GetChild(2).transform.GetComponent<EnemyFieldOfView>().ViewAngle-=2;
        GameObject.Find("EnemyBoss").transform.GetChild(0).GetChild(2).transform.GetComponent<EnemyFieldOfView>().ViewRadius-=0.5f;
    }

    public void BossLevelSystem(){
        if(GameObject.Find("EnemyBoss").transform.GetChild(0).gameObject.active == true && BossMovement.instance.myFriendNumber > 0){
            ClosestTarget.gameObject.SetActive(false);
            BossMovement.instance.myFriendNumber--;
            GameManager.instance.player.transform.localScale += new Vector3(0.2f,0.2f,0.2f);
            ViewRadius += 0.5f;
            ViewAngle += 1;
            GameManager.instance.upScalePartical.Play();
        }
    }

    public async void ClosestTargetColliderFalse(){

        if(PlayerController.instance.playerMyHealth + 1 >= ClosestTarget.transform.GetComponent<EnemyMovement>().myHealth && ClosestTarget.transform.GetComponent<EnemyMovement>().myHealth <= 0 && GameObject.Find("EnemyBoss").transform.GetChild(0).gameObject.active == false){
            //ClosestTarget.transform.position = GameManager.instance.player.transform.position;
            //GameManager.instance.player.transform.DOScale(ClosestTarget.transform.localScale,1f);
            //GameManager.instance.player.transform.DOPunchScale(ClosestTarget.transform.localScale,1);
            //GameManager.instance.player.transform.DOShakeScale(1,ClosestTarget.transform.localScale,1);
            Debug.Log("Come onnnn");
            GameManager.instance.upScalePartical.Play();
            GameManager.instance.player.transform.DOScale(ClosestTarget.transform.localScale,2f).SetEase(Ease.InOutBounce);
            ViewRadius += 2;
            ViewAngle += 5;
            //GameManager.instance.player.transform.DORewind();
            //GameManager.instance.player.transform.DOPunchScale(new Vector3(GameManager.instance.player.transform.localScale.x,GameManager.instance.player.transform.localScale.y,GameManager.instance.player.transform.localScale.z),1f);
            PlayerController.instance.playerMyHealth++;
            ClosestTarget.gameObject.layer = 0;
            ClosestTarget.GetComponent<CapsuleCollider>().enabled = false;
            //ClosestTarget.transform.GetChild(2).GetComponent<SkinnedMeshRenderer>().material.DOColor(GameManager.instance.friendMaterial.color,3);
            ClosestTarget.transform.GetChild(2).GetComponent<SkinnedMeshRenderer>().material = GameManager.instance.friendMaterial; 
           //ClosestTarget.transform.GetChild(6).gameObject.transform.parent = null;
            ClosestTarget.transform.GetChild(3).gameObject.SetActive(false);
            ClosestTarget.transform.GetChild(4).gameObject.SetActive(true);
            //ClosestTarget.transform.GetChild(5).gameObject.SetActive(true);
            ClosestTarget.transform.GetChild(6).gameObject.SetActive(true);
            ClosestTarget.transform.GetChild(7).gameObject.SetActive(true);
            ClosestTarget.transform.GetChild(8).gameObject.SetActive(true);
            ClosestTarget.transform.GetChild(9).gameObject.SetActive(true);
            ClosestTarget.transform.GetChild(10).gameObject.SetActive(true);
            ClosestTarget.transform.GetChild(4).gameObject.GetComponent<ParticleSystem>().Play();
            ClosestTarget.transform.GetChild(5).gameObject.GetComponent<ParticleSystem>().Play();
            ClosestTarget.transform.GetChild(6).gameObject.GetComponent<ParticleSystem>().Play();
            ClosestTarget.transform.GetChild(4).gameObject.transform.parent = null;
            ClosestTarget.transform.GetChild(5).gameObject.transform.parent = null;
            ClosestTarget.transform.GetChild(6).gameObject.transform.parent = null;
            ClosestTarget.transform.GetChild(7).gameObject.transform.parent = null;
            //ClosestTarget.transform.GetChild(8).gameObject.transform.parent = null;
            ClosestTarget.transform.GetChild(ClosestTarget.transform.childCount - 1).gameObject.transform.parent = null;
            ClosestTarget.transform.GetChild(ClosestTarget.transform.childCount - 2).gameObject.transform.parent = null;
            //ClosestTarget.transform.GetChild(ClosestTarget.transform.childCount - 3).gameObject.transform.parent = null;
            ClosestTarget.transform.DORotate(new Vector3(transform.rotation.x, transform.rotation.y - 720, transform.rotation.z), 0.7f, RotateMode.LocalAxisAdd);
            //ClosestTarget.gameObject.SetActive(false);
            //ClosestTarget.transform.parent = GameManager.instance.player.transform;
            //Sonradan eklendirler
            ClosestTarget.transform.GetComponent<EnemyMovement>().enabled = false;
            ClosestTarget.transform.GetComponent<NavMeshAgent>().enabled = false;
            //ClosestTarget.transform.GetComponent<EnemyJoystickMovement>().enabled = false;
            ClosestTarget.transform.GetComponent<EnemyRandomMovement>().enabled = false;
            ClosestTarget.transform.GetChild(0).gameObject.SetActive(true);
            ClosestTarget.transform.GetChild(0).parent = null;
            SliderBarSystem.instance.PositionUpY();
            //ClosestTarget.transform.position = new Vector3(GameManager.instance.player.transform.position.x - 2,GameManager.instance.player.transform.position.y,GameManager.instance.player.transform.position.z -2);
            //ClosestTarget.transform.rotation = GameManager.instance.player.transform.rotation;
        }

     
        //ClosestTarget.GetComponent<CapsuleCollider>().enabled = false;

        //ClosestTarget.gameObject.layer = 0;//Düşmanın layerını değiştirdimki tekrar tekrar ateş etmesin

    
        
        Debug.Log("bEN ÇAĞRILDIM");
        //ClosestTarget.transform.parent = GameManager.instance.player.transform;//Childı yaptım lakin bu çok kötü oldu
    }
    

    IEnumerator OneSecondFunction(){
        yield return new WaitForSeconds(1f);
        ClosestTarget.transform.DOScale(0,1);
        ClosestTarget.GetChild(2).GetComponent<EnemyFieldOfView>().enabled = false;
        ClosestTarget.GetComponent<CapsuleCollider>().enabled = false;
    }

    private void RemoveFOV() {
        DrawFieldOfView = false;
        ViewMeshFilter.transform.GetComponent<MeshRenderer>().enabled = false;
    }

    
    IEnumerator WaitFunction(){
        yield return new WaitForSeconds(1f);
        ClosestTarget.GetComponent<Animator>().SetBool("fail",true);
    }

    private void DrawFOV() {
		int stepCount = Mathf.RoundToInt(ViewAngle * MeshResolution);
		float stepAngleSize = ViewAngle / stepCount;
		List<Vector3> viewPoints = new List<Vector3> ();
		ViewCastInfo oldViewCast = new ViewCastInfo ();
		for (int i = 0; i <= stepCount; i++) {
			float angle = transform.eulerAngles.y - ViewAngle / 2 + stepAngleSize * i;
			ViewCastInfo newViewCast = ViewCast (angle);

			if (i > 0) {
				bool edgeDstThresholdExceeded = Mathf.Abs (oldViewCast.Dst - newViewCast.Dst) > EdgeDstThreshold;
				if (oldViewCast.Hit != newViewCast.Hit || (oldViewCast.Hit && newViewCast.Hit && edgeDstThresholdExceeded)) {
					EdgeInfo edge = FindEdge (oldViewCast, newViewCast);
					if (edge.PointA != Vector3.zero) {
						viewPoints.Add (edge.PointA);
					}
					if (edge.PointB != Vector3.zero) {
						viewPoints.Add (edge.PointB);
					}
				}

			}


			viewPoints.Add (newViewCast.Point);
			oldViewCast = newViewCast;
		}

		int vertexCount = viewPoints.Count + 1;
		Vector3[] vertices = new Vector3[vertexCount];
		int[] triangles = new int[(vertexCount-2) * 3];

		vertices [0] = Vector3.zero;
		for (int i = 0; i < vertexCount - 1; i++) {
			vertices [i + 1] = transform.InverseTransformPoint(viewPoints [i]);

			if (i < vertexCount - 2) {
				triangles [i * 3] = 0;
				triangles [i * 3 + 1] = i + 1;
				triangles [i * 3 + 2] = i + 2;
			}
		}

		_viewMesh.Clear ();

		_viewMesh.vertices = vertices;
		_viewMesh.triangles = triangles;
		_viewMesh.RecalculateNormals ();
    }

    private ViewCastInfo ViewCast(float globalAngle) {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit, ViewRadius, ObstacleMask)) {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);  
        }
        else {
            return new ViewCastInfo(false, transform.position + dir * ViewRadius, ViewRadius, globalAngle);
        }
    }

    private EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast) {
		float minAngle = minViewCast.Angle;
		float maxAngle = maxViewCast.Angle;
		Vector3 minPoint = Vector3.zero;
		Vector3 maxPoint = Vector3.zero;

		for (int i = 0; i < EdgeResolveIterations; i++) {
			float angle = (minAngle + maxAngle) / 2;
			ViewCastInfo newViewCast = ViewCast (angle);

			bool edgeDstThresholdExceeded = Mathf.Abs (minViewCast.Dst - newViewCast.Dst) > EdgeDstThreshold;
			if (newViewCast.Hit == minViewCast.Hit && !edgeDstThresholdExceeded) {
				minAngle = angle;
				minPoint = newViewCast.Point;
			} else {
				maxAngle = angle;
				maxPoint = newViewCast.Point;
			}
		}

		return new EdgeInfo (minPoint, maxPoint);
	}

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) {
        if (!angleIsGlobal) {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public struct ViewCastInfo 
    {
        public bool Hit;
        public Vector3 Point;
        public float Dst;
        public float Angle;

        public ViewCastInfo(bool hit, Vector3 point, float dst, float angle) {
            Hit = hit;
            Point = point;
            Dst = dst;
            Angle = angle;
        }    
    }

    public struct EdgeInfo {
		public Vector3 PointA;
		public Vector3 PointB;

		public EdgeInfo(Vector3 pointA, Vector3 pointB) {
			PointA = pointA;
			PointB = pointB;
		}
	}
}
