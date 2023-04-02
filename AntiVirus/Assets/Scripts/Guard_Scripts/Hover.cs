using UnityEngine;

public class Hover : MonoBehaviour
{
    private Rigidbody self;
    [SerializeField] private float hoverHeight;
    // Start is called before the first frame update
    void Awake()
    {
        self = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hover();
    }
    private void hover(){
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 20)){
            // Finds height above closest gameObject below
            float height = gameObject.transform.position.y - hit.transform.position.y;

            // If we are below our chosen height we accelerate vertically
            if (height < hoverHeight - 0.1){
                self.velocity += Vector3.up * 13f * Time.deltaTime;
            }

            // If we are above our height AND our vertical velocity is still positive we apply a downward force 
            if (height > hoverHeight + 0.1 && self.velocity.y > 0){
                self.velocity += Vector3.down * Time.deltaTime;
            }

            // This balances force of gravity within a small range of our height
            if (Mathf.Abs(height - hoverHeight) < 0.1 && self.velocity.y < 1){
                self.velocity += Vector3.up * 9.811f * Time.deltaTime;
            }
        }
    }
}
