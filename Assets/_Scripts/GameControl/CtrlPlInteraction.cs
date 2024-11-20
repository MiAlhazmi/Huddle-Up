using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CtrlPlInteraction : MonoBehaviour
{
    private GameControl _gameCtrl;
    private SoundEffectsControl _soundEffectCtrl;
    public float pushDistance = 2f; // Distance the hit player will be pushed back
    public float pushUpDistance = 3;
    public float pushDuration = 0.5f; // Time over which to push the player
    // [SerializeField] private float _pushforce = 10f;

    private void Awake()
    {
        _gameCtrl = GetComponent<GameControl>();
        _soundEffectCtrl = GetComponent<SoundEffectsControl>();
    }

    public void HasHit(GameObject player, GameObject target)
    {
        Vector3 hitDirection = (target.transform.position - player.transform.position).normalized;
        DamagePlayer(hitDirection, target);
        if (IsTagger(player))
        {
            TransferTag(player, target);
        }
    }

    private bool IsTagger(GameObject player)
    {
        if (_gameCtrl.GetTagOwner().IsUnityNull()) return false;
        return player.GetInstanceID() == _gameCtrl.GetTagOwner().GetInstanceID();
        // or player.GetComponent<Player>.GetIsTagger()
    }

    // hit direction helps to identify which direction the target would be pushed towards
    private void DamagePlayer(Vector3 hitDirection, GameObject target)
    {
        // TODO: we don't need to pass the whole object of the target we just need the CharacterController
        // target.GetComponent<PlayerHit>().GetHit(hitDirection);
        // target.transform.position += (Vector3.up * 3);
        // target.GetComponent<CharacterController>().Move((10 * hitDirection + Vector3.up * 2) * Time.deltaTime);
        
        // Calculate new position for the enemy after the push
        // Vector3 newPosition = target.transform.position + hitDirection * pushDistance;
        // StartCoroutine(SmoothPush(target.transform, newPosition));

        // Calculate push vector
        Vector3 pushVector = IsTagger(target) ? hitDirection * pushDistance * 10 : hitDirection * pushDistance;
        
        // Start the push coroutine to move the player smoothly
        StartCoroutine(SmoothPush(target.GetComponent<CharacterController>(), pushVector));


        // Rigidbody targetRb = target.GetComponent<Rigidbody>();
        // targetRb.AddForce(hitDirection * _pushforce, ForceMode.Impulse);
        
        target.GetComponent<PlayerUI>().TakeDamageUI();
    }

    private IEnumerator SmoothPush(CharacterController target, Vector3 pushVector)
    {
        Vector3 originalVelocity = Vector3.zero;
        float elapsedTime = 0f;

        while (elapsedTime < pushDuration)
        {
            // Calculate the amount of movement based on elapse timed
            Vector3 currentMove = Vector3.Lerp(originalVelocity, pushVector, elapsedTime / pushDuration) * Time.deltaTime;
            Debug.Log(currentMove);
            target.Move(currentMove);

            // target.Move(hitVector * Time.deltaTime);
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Ensure final movement to complete push
        target.Move(pushVector * Time.deltaTime);
    }

    // IEnumerator SmoothPush(Transform target, Vector3 destination)
    // {
    //     Vector3 startingPosition = target.position;
    //     float elapsedTime = 0f;
    //     
    //     while (elapsedTime < pushDuration)
    //     {
    //         target.position = Vector3.Lerp(startingPosition, destination, elapsedTime / pushDuration);
    //         elapsedTime += Time.deltaTime;
    //         yield return null;
    //     }
    //
    //     target.position = destination; // Ensure final position is set
    // }

    private void TransferTag(GameObject fromPlayer, GameObject toTarget)
    {
        _soundEffectCtrl.PlayTagTransfer();   // play TagTransfer sound
        fromPlayer.GetComponent<Player>().SetIsTagger(false);
        toTarget.GetComponent<Player>().SetIsTagger(true);
        _gameCtrl.SetTagOwner(toTarget);
    }
}
