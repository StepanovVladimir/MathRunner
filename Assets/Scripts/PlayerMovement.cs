using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public GameManager GM;
    public TextMesh equationText;
    public float jumpSpeed;
    public GameObject[] answers;
    public Text[] answersTexts;

    Animator animator;
    Rigidbody rb;

    bool wannaJump = false;

    int indexOfRightAnswer;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.AddForce(new Vector3(0, Physics.gravity.y * 3, 0), ForceMode.Acceleration);

        if (wannaJump && isGrounded())
        {
            animator.SetTrigger("jumping");
            rb.AddForce(new Vector3(0, jumpSpeed, 0), ForceMode.VelocityChange);
            wannaJump = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Trap"))
        {
            return;
        }

        Destroy(collision.gameObject);
        StartCoroutine(Damage());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MathBegin"))
        {
            int answer;
            if (Random.Range(0, 2) == 0)
            {
                int firstNumber = Random.Range(10, 50);
                int secondNumber = Random.Range(10, 50);
                answer = firstNumber + secondNumber;
                equationText.text = $"{firstNumber} + {secondNumber} = ?";
            }
            else
            {
                int firstNumber = Random.Range(20, 100);
                int secondNumber = Random.Range(10, firstNumber - 9);
                answer = firstNumber - secondNumber;
                equationText.text = $"{firstNumber} - {secondNumber} = ?";
            }

            int[] wrongAnswers = new int[2];
            if (Random.Range(0, 2) == 0)
            {
                wrongAnswers[0] = answer - Random.Range(1, 5);
            }
            else
            {
                wrongAnswers[0] = answer + Random.Range(1, 5);
            }

            if (Random.Range(0, 2) == 0)
            {
                do
                {
                    wrongAnswers[1] = answer - Random.Range(1, 5);
                }
                while (wrongAnswers[1] == wrongAnswers[0]);
            }
            else
            {
                do
                {
                    wrongAnswers[1] = answer + Random.Range(1, 5);
                }
                while (wrongAnswers[1] == wrongAnswers[0]);
            }

            indexOfRightAnswer = Random.Range(0, answers.Length);
            int wrongAnswerIndex = 0;
            for (int i = 0; i < answers.Length; i++)
            {
                answers[i].SetActive(true);
                if (i == indexOfRightAnswer)
                {
                    answersTexts[i].text = answer.ToString();
                }
                else
                {
                    answersTexts[i].text = wrongAnswers[wrongAnswerIndex].ToString();
                    wrongAnswerIndex++;
                }
            }
        }
        else if (other.CompareTag("MathEnd"))
        {
            if (equationText.text != "")
            {
                equationText.text = "";
                for (int i = 0; i < answers.Length; i++)
                {
                    answers[i].SetActive(false);
                }
                StartCoroutine(Damage());
            }
        }
        else if (other.CompareTag("End"))
        {
            GM.canPlay = false;
            animator.SetTrigger("win");
        }
    }

    public void Jump()
    {
        if (isGrounded() && GM.canPlay)
        {
            wannaJump = true;
        }
    }

    public void GiveAnswer(int index)
    {
        equationText.text = "";
        for (int i = 0; i < answers.Length; i++)
        {
            answers[i].SetActive(false);
        }

        if (index != indexOfRightAnswer)
        {
            StartCoroutine(Damage());
        }
    }

    IEnumerator Damage()
    {
        GM.canPlay = false;
        animator.SetTrigger("damage");

        yield return new WaitForSeconds(2.85f);

        GM.canPlay = true;
        animator.ResetTrigger("damage");
    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.05f);
    }
}
