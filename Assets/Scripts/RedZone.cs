using UnityEngine ;

public class RedZone : MonoBehaviour {
   private void OnTriggerStay (Collider other) {
      Cube cube = other.GetComponent <Cube> () ;
      if (cube != null) {
         if (!cube.IsMainCube && cube.CubeRigidbody.velocity.magnitude < .1f) {
            Debug.Log ("Gameover") ;
         }
      }
   }
}
