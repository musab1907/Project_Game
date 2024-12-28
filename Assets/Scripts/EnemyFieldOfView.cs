using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
using TapticPlugin;
public class EnemyFieldOfView : MonoBehaviour
{
    public float ViewRadius;
    [Range(0, 360)] public float ViewAngle;
    public LayerMask TargetMask;
    public LayerMask ObstacleMask;

    public ParticleSystem GunFire;
    public bool DrawFieldOfView;
	public MeshFilter ViewMeshFilter;
    public float MeshResolution;
    public int EdgeResolveIterations;
	public float EdgeDstThreshold;

    public bool onTriggerValue = false;

    public Material targetColor;

    public int shotValue;
    
    [NonSerialized] public Transform ClosestTarget;

    //private Soldier _soldier;
	private Mesh _viewMesh;

    public static EnemyFieldOfView instance;
    private void Start() {
        _viewMesh = new Mesh ();
		_viewMesh.name = "View Mesh";
		ViewMeshFilter.mesh = _viewMesh;
        //GameManager.instance.enemyMaterial = ClosestTarget.GetComponent<Renderer>().material;
        //_soldier = GetComponent<Soldier>();
        instance = this;
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


    private void FindVisibleTargets() {
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

                        TapticPlugin.TapticManager.Impact(ImpactFeedback.Medium);
                        EnemyDeadPlayerFunction();
                        //GameManager.instance.gunFire.Play();
                        Debug.Log("Düşman");
                        GunFire.Play();
                        //transform.LookAt(GameManager.instance.player.transform);
                        transform.parent.LookAt(GameManager.instance.player.transform);
                        
                        ClosestTarget.transform.GetChild(10).GetComponent<ParticleSystem>().Play();

                        if(GameObject.Find("EnemyBoss").transform.GetChild(0).transform.gameObject.active == true){
                            BossDeadPlayer();
                        }
                        
                        //ClosestTarget.GetComponent<CapsuleCollider>().enabled = false;
                        //ClosestTarget.GetComponent<Renderer>().material.DOColor(Color.blue,1); 
                        //ClosestTarget.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.DOColor(GameManager.instance.friendMaterial.color,1);
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


    public void EnemyDeadPlayerFunction(){
        if(GameObject.Find("EnemyBoss").transform.GetChild(0).gameObject.active == false){
           if(transform.parent.GetComponent<EnemyMovement>().myHealth > ClosestTarget.transform.GetComponent<PlayerController>().playerMyHealth){//Ebeveynimin içindeki health büyükse 
             SliderBarSystem.instance.NegativeUseMoney(10);
            //DOVirtual.DelayedCall(0.2f,SliderBarSystem.instance.PlayerDead);
            //SliderBarSystem.instance.NegativeUseMoney(100);
        }
        }
    }

    public void BossDeadPlayer(){
        SliderBarSystem.instance.NegativeUseMoney(5);
        GameManager.instance.player.transform.localScale -= new Vector3(0.2f,0.2f,0.2f);
        GameManager.instance.player.transform.GetChild(0).GetChild(5).transform.GetComponent<FieldOfView>().ViewAngle -= 2;
        GameManager.instance.player.transform.GetChild(0).GetChild(5).transform.GetComponent<FieldOfView>().ViewRadius -= 0.5f;
    }
    private void RemoveFOV() {
        DrawFieldOfView = false;
        ViewMeshFilter.transform.GetComponent<MeshRenderer>().enabled = false;
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
