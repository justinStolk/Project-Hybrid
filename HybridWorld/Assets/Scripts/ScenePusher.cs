using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class ScenePusher : MonoBehaviour
{
    private VideoPlayer introVideo;
    private bool prepped;

    private void Start()
    {
        introVideo = this.GetComponent<VideoPlayer>();
    }
    // Update is called once per frame
    void Update()
    {
        if (introVideo.isPlaying)
        {
            prepped = true;
        }
        if (!introVideo.isPlaying && prepped)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}