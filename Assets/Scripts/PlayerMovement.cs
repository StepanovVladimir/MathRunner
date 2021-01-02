using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public GameManager GM;
    public int equationComplex;
    public Animator animator;
    public GameObject startMenu;
    public GameObject jumpButton;
    public TextMesh equationText;
    public TextMesh equationText1;
    public GameObject[] answers;
    public Text[] answersTexts;

    Rigidbody rb;
    float jumpSpeed = 12;
    bool wannaJump = false;

    int indexOfRightAnswer;

    void Start()
    {
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
                answer = Random.Range(equationComplex * 2, equationComplex * 4 - 1);
                int firstNumber = Random.Range(equationComplex, answer - equationComplex + 1);
                int secondNumber = answer - firstNumber;
                equationText.text = $"{firstNumber} + {secondNumber} = ?";
                equationText1.text = equationText.text;
            }
            else
            {
                int firstNumber = Random.Range(equationComplex * 2, Mathf.Min(equationComplex * 4 - 1, 21));
                int secondNumber = Random.Range(equationComplex, firstNumber - equationComplex + 1);
                answer = firstNumber - secondNumber;
                equationText.text = $"{firstNumber} - {secondNumber} = ?";
                equationText1.text = equationText.text;
            }

            int[] wrongAnswers = new int[2];
            if (Random.Range(0, 2) == 0)
            {
                wrongAnswers[0] = answer - Random.Range(1, 4);
            }
            else
            {
                wrongAnswers[0] = answer + Random.Range(1, 4);
            }

            if (Random.Range(0, 2) == 0)
            {
                do
                {
                    wrongAnswers[1] = answer - Random.Range(1, 4);
                }
                while (wrongAnswers[1] == wrongAnswers[0]);
            }
            else
            {
                do
                {
                    wrongAnswers[1] = answer + Random.Range(1, 4);
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
                equationText1.text = "";
                for (int i = 0; i < answers.Length; i++)
                {
                    answers[i].SetActive(false);
                }
                StartCoroutine(Damage());
            }
        }
        else if (other.CompareTag("End"))
        {
            StartCoroutine(Win());
        }
    }

    public void StartRun()
    {
        GM.canPlay = true;
        animator.SetTrigger("start");
        startMenu.SetActive(false);
        jumpButton.SetActive(true);
    }

    public void Lose()
    {
        animator.SetTrigger("lose");
        jumpButton.SetActive(false);
        if (equationText.text != "")
        {
            equationText.text = "";
            equationText1.text = "";
            for (int i = 0; i < answers.Length; i++)
            {
                answers[i].SetActive(false);
            }
        }
    }

    public void Jump()
    {
        if (isGrounded() && GM.canPlay && !GM.damage)
        {
            wannaJump = true;
        }
    }

    public void GiveAnswer(int index)
    {
        equationText.text = "";
        equationText1.text = "";
        for (int i = 0; i < answers.Length; i++)
        {
            answers[i].SetActive(false);
        }

        if (index != indexOfRightAnswer)
        {
            StartCoroutine(Damage());
        }
    }

    IEnumerator Win()
    {
        GM.canPlay = false;
        animator.SetTrigger("win");
        jumpButton.SetActive(false);

        yield return new WaitForSeconds(1.55f);

        GM.Win();
    }

    IEnumerator Damage()
    {
        GM.damage = true;
        animator.SetTrigger("damage");

        yield return new WaitForSeconds(2.85f);

        GM.damage = false;
        animator.ResetTrigger("damage");
    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.05f);
    }
}
