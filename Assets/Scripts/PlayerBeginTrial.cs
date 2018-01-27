﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeginTrial : TriggerPlayerAction
{
    public GameObject RewardItem;
    public List<Vector2> TargetPosition;

    protected override void OnActionTrigger(GameObject player)
    {
        foreach (var p in TargetPosition)
        {
            var item = Instantiate(RewardItem);
            Destroy(item.GetComponent<CircleCollider2D>());
            var t = item.AddComponent<TravelToTarget>();
            t.transform.position = transform.position;
            t.Target = p;
            t.Speed = 10;
            t.AtDestination(pos => {
                Debug.Log("Arrived at : " + pos);
                var collider = item.AddComponent<CircleCollider2D>();
                collider.isTrigger = true;
                item.AddComponent<TrialPickup>();
            });
        }
        var collector = player.AddComponent<TrialPickupCollector>();
        collector.PickupGoal = TargetPosition.Count;
        Destroy(this);
        gameObject.AddComponent<PlayerCompleteTrial>();
        Debug.Log("To complete: " + collector.PickupGoal);
    }
}