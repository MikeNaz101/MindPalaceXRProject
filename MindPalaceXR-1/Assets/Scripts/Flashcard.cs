using UnityEngine;

public class Flashcard : MonoBehaviour
{
    public Renderer flashcardRenderer;
    public ParticleSystem pickupParticles;
    //public Mesh[] possibleShapes; // Assign an array of Mesh assets in the Inspector
    //public Sprite[] sprites; // Assign an array of Sprite assets
    public AudioSource pickupSound;
    public AudioClip[] pickupSounds;
    public OVRInput.Controller hapticsController;

    [System.Serializable]
    public struct VibrationPattern
    {
        public float frequency;
        public float amplitude;
        public float duration;
    }
    private VibrationPattern vibrationPattern; // Store the pattern directly
    private bool hasPlayed = false;

    void Start()
    {
        // Get the ParticleSystem's Renderer module
        var renderer = pickupParticles.GetComponent<ParticleSystemRenderer>();

        // Get the ParticleSystem's Material
        Material particleMaterial = renderer.material;

        // Randomize metallic and smoothness
        particleMaterial.SetFloat("_Metallic", Random.Range(0f, 1f));
        particleMaterial.SetFloat("_Glossiness", Random.Range(0f, 1f));
        
        // Get the ParticleSystem's Trail module
        var trails = pickupParticles.trails;

        // 50/50 chance to enable trails
        if (Random.value > 0.5f)
        {
            trails.enabled = true;
            

            // Enable trails
            trails.enabled = true;

            // Randomize trail properties (example)
            trails.colorOverLifetime = new ParticleSystem.MinMaxGradient(Color.white, Color.red);
            trails.widthOverTrail = new ParticleSystem.MinMaxCurve(1f, new AnimationCurve(new Keyframe(0f, 1f), new Keyframe(1f, 0f)));
        }

        // Choose a random sprite from the array
        //int randomSpriteIndex = Random.Range(0, sprites.Length);
        //renderer.SetSprites(new Sprite[] { sprites[randomSpriteIndex] });

        // Change the flashcard's color
        Color randomColor = GetRandomColor();
        flashcardRenderer.material.color = randomColor;

        // Set particle color to a similar hue
        var main = pickupParticles.main;
        main.startColor = Random.ColorHSV(randomColor.r, randomColor.r, 0.5f, 1f, 0.5f, 1f); // Adjust values as needed

        // Randomize particle system properties
        var emission = pickupParticles.emission;
        emission.rateOverTime = Random.Range(50f, 100f); // randomize emission rate

        //var shape = pickupParticles.shape;// Choose a random shape from the array
        //int randomIndex = Random.Range(0, possibleShapes.Length);
        //shape.mesh = possibleShapes[randomIndex];
        // Enable mesh shape type
        //shape.shapeType = ParticleSystemShapeType.Mesh;

        var size = pickupParticles.sizeOverLifetime;
        size.sizeMultiplier = Random.Range(0.5f, 2.5f); // randomize size

        int randomIndex = Random.Range(0, pickupSounds.Length);
        pickupSound.clip = pickupSounds[randomIndex];

        // Generate a random vibration pattern for this card
        vibrationPattern = new VibrationPattern
        {
            frequency = Random.Range(0.1f, 1f), // Adjust range as needed
            amplitude = Random.Range(0.1f, 1f), // Adjust range as needed
            duration = Random.Range(0.1f, 0.5f) // Adjust range as needed
        };
    }

    void Update()
    {
        //while (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, hapticsController))
        if (GetComponent<OVRGrabbable>().isGrabbed)
        {
            if (!hasPlayed) // Do i want it to play the sound continueously?
            {
                //int randomIndex = Random.Range(0, pickupSounds.Length);
                //pickupSound.clip = pickupSounds[randomIndex];
                pickupSound.Play();

                hasPlayed = true;
            }
            OVRInput.SetControllerVibration(vibrationPattern.frequency, vibrationPattern.amplitude, hapticsController);
            pickupParticles.Play();
            //OVRInput.SetControllerVibration(0.5f, 0.5f, hapticsController);
        }
        else
        {
            OVRInput.SetControllerVibration(0f, 0f, hapticsController);
            hasPlayed = false;
        }

        while (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, hapticsController))
        {
            OVRInput.SetControllerVibration(0f, 0f, hapticsController);
        }
    }

    Color GetRandomColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }
}