using UnityEngine ;

public class CubeCollision : MonoBehaviour {
   Cube cube ;

   private void Awake () {
      cube = GetComponent<Cube> () ;
   }

   private void OnCollisionEnter (Collision collision) {
      Cube otherCube = collision.gameObject.GetComponent <Cube> () ;

      // check if contacted with other cube
      if (otherCube != null && cube.CubeID > otherCube.CubeID) {
         // check if both cubes have same number
         if (cube.CubeNumber == otherCube.CubeNumber) {
            Vector3 contactPoint = collision.contacts [ 0 ].point ;

            // check if cubes number less than max number in CubeSpawner:
            if (otherCube.CubeNumber < CubeSpawner.Instance.maxCubeNumber) {
               // spawn a new cube as a result
               Cube newCube = CubeSpawner.Instance.Spawn (cube.CubeNumber * 2, contactPoint + Vector3.up * 1.6f) ;
               //push the new cube up and forward:
               float pushForce = 2.5f ;
               newCube.CubeRigidbody.AddForce (new Vector3 (0, .3f, 1f) * pushForce, ForceMode.Impulse) ;

               // add some torque:
               float randomValue = Random.Range (-20f, 20f) ;
               Vector3 randomDirection = Vector3.one * randomValue ;
               newCube.CubeRigidbody.AddTorque (randomDirection) ;
            }

            // the explosion should affect surrounded cubes too:
            Collider[] surroundedCubes = Physics.OverlapSphere (contactPoint, 2f) ;
            float explosionForce = 400f ;
            float explosionRadius = 1.5f ;

            foreach (Collider coll in surroundedCubes) {
               if (coll.attachedRigidbody != null)
                  coll.attachedRigidbody.AddExplosionForce (explosionForce, contactPoint, explosionRadius) ;
            }

            FX.Instance.PlayCubeExplosionFX (contactPoint, cube.CubeColor) ;

            // Destroy the two cubes:
            CubeSpawner.Instance.DestroyCube (cube) ;
            CubeSpawner.Instance.DestroyCube (otherCube) ;
         }
      }
   }
}
