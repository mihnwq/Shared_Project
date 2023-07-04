using UnityEngine;

public class Jumping
{
    public float jumpForce = 7f;

    public float extendedJumpForce = 11f;

     Transform tf;
     Rigidbody rb;
    
    public Jumping(Transform tf, Rigidbody rb)
    {
        this.tf = tf;
        this.rb = rb;
    }

    float finalJumpForce;

    float smoothJumpTime = 4f;

    public void Jump()
    {


          rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);



          rb.AddForce(tf.up * jumpForce, ForceMode.Impulse);

      
    }
    
}
