using UnityEngine;
using HutongGames.PlayMaker;

namespace MSCSC
{
    public static class GetFireworks
    {
        public static void SpawnRocket(GameObject spawn_object, GameObject spawn_at_position)
        {
            GameObject firew = UnityEngine.Object.Instantiate(spawn_object);
            firew.transform.position = spawn_at_position.transform.position + spawn_at_position.transform.forward * 1f + spawn_at_position.transform.up * 1f;
            Vector3 vector = new Vector3(UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f));
            firew.transform.localEulerAngles = vector;
            firew.transform.FindChild("Trigger").GetComponents<PlayMakerFSM>()[0].SendRemoteFsmEvent("EXPLOSION");
        }
    }
}
