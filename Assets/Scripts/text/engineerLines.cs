using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class engineerLines : MonoBehaviour
{
    // Start is called before the first frame update
    public List<string> opening;
    public List<string> itemReturn;
    public List<string> correctItems;
    public List<string> incorrectItems;
    public List<string> leaving;

    public TextMeshProUGUI textBox;
    public Speech _speech;

    public float textSpeed = 0.04f;
    public itemTracker _itemTracker;
    public inventory _inventory;
    public Player _player;
    public ProximityTrigger _prox;
    public bool finished = false;
    public GameObject partsNeeded;
    
    void Start()
    {
        StartCoroutine(begin());
        partsNeeded.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void processLines(string[] line){
            switch(line[3]){
                case "opening":
                    opening.Add(line[5]);
                    break;
                case "if correct items":
                    correctItems.Add(line[5]);
                    break;
                case "if incorrect items":
                    incorrectItems.Add(line[5]);
                    break;
                case "if plane finished":
                    leaving.Add(line[5]);
                    break;
                default:
                    itemReturn.Add(line[5]);
                    break;
            }
    }

    IEnumerator begin(){
        yield return new WaitForSeconds(0.1f);
        textBox.text = "";

        for(int q= 0; q<opening.Count; q++){
            textBox.text = "";
            StartCoroutine(typing(opening[q]));
            yield return new WaitForSeconds ((textSpeed*opening[q].Length)+1.5f);
            if (q==4){
                partsNeeded.SetActive(true);
            }
        }
        _speech.HideSpeechBubble();
        _player.canMove = true;
        _prox.started = true;
    }

    public void callTyping(bool click, int index){
        StartCoroutine(setTyping(click, index));
        Debug.Log(click + " " + index);
    }

    IEnumerator setTyping(bool click, int index){
        if(!click){
            _player.canMove = false;
            StartCoroutine(typing(itemReturn[index]));
            yield return new WaitForSeconds ((textSpeed*itemReturn[index].Length)+1.5f);
            _player.canMove = true;
        }else{
            _player.canMove = false;
            if( _itemTracker.checkIsNeeded(_inventory.inv)){
                StartCoroutine(typing(correctItems[index]));
                yield return new WaitForSeconds ((textSpeed*correctItems[index].Length)+1.5f);
                StartCoroutine(typing(correctItems[index+1]));
                yield return new WaitForSeconds ((textSpeed*correctItems[index+1].Length)+1.5f);
            } else {
                StartCoroutine(typing(incorrectItems[index]));
                yield return new WaitForSeconds ((textSpeed*incorrectItems[index].Length)+1.5f);
                StartCoroutine(typing(incorrectItems[index+1]));
                yield return new WaitForSeconds ((textSpeed*incorrectItems[index+1].Length)+1.5f);
            }
            if(finished){
                StartCoroutine(ending());
            }else{
                _player.canMove = true;
            }
        }
        _speech.HideSpeechBubble();
    }

    IEnumerator ending(){
        _speech.DisplayClickText();
        for(int q= 0; q<leaving.Count; q++){
            textBox.text = "";
            StartCoroutine(typing(leaving[q]));
            yield return new WaitForSeconds ((textSpeed*leaving[q].Length)+1.5f);
        }
        
    }

    IEnumerator typing(string str) {
        Debug.Log(str);

        textBox.text = "";
        foreach (char c in str) {
			textBox.text += c;
			yield return new WaitForSeconds (textSpeed);
		}
    }
}
