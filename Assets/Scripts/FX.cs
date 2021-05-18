using UnityEngine ;

public class FX : MonoBehaviour {
   [SerializeField] private ParticleSystem cubeExplosionFX ;

   ParticleSystem.MainModule cubeExplosionFXMainModule ;

   //singleton class
   public static FX Instance ;

   private void Awake () {
      Instance = this ;
   }

   private void Start () {
      cubeExplosionFXMainModule = cubeExplosionFX.main ;
   }

   public void PlayCubeExplosionFX (Vector3 position, Color color) {
      cubeExplosionFXMainModule.startColor = new ParticleSystem.MinMaxGradient (color) ;
      cubeExplosionFX.transform.position = position ;
      cubeExplosionFX.Play () ;
   }

}
