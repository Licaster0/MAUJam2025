using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private List<Transform> coins; // Dinamik liste
    [SerializeField] private float scatterRadius = 2f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int coinValue;

    private Player player;

    private void Start()
    {
        player = PlayerManager.instance.player;

        // Coin'leri rastgele pozisyonlara dağıt
        foreach (Transform coin in coins)
        {
            Vector3 randomOffset = Random.insideUnitCircle * scatterRadius;
            Vector3 spawnPosition = transform.position + randomOffset;

            coin.position = spawnPosition;
        }

        StartCoroutine(RbDisabled());
    }

    private void Update()
    {
        StartCoroutine(CollectCoin());
    }

    private IEnumerator RbDisabled()
    {
        yield return new WaitForSeconds(1f);

        foreach (Transform coin in coins)
        {
            if (coin == null) continue; // 
            coin.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    private IEnumerator CollectCoin()
    {
        yield return new WaitForSeconds(1.3f);

        while (coins.Count > 0)
        {
            for (int i = coins.Count - 1; i >= 0; i--)
            {
                Transform coin = coins[i];

                if (coin == null) continue;


                coin.position = Vector3.MoveTowards(coin.position, player.transform.position, moveSpeed * Time.deltaTime);


                if (Vector3.Distance(player.transform.position, coin.position) < 0.1f)
                {
                    player.fx.PlayCoinFX();
                    Destroy(coin.gameObject);
                    PlayerManager.instance.Coin += coinValue;
                    AudioManager.instance.PlaySFX(8, null);
                    coins.RemoveAt(i);
                }

                if (coins.Count == 0)
                {
                    Destroy(gameObject);
                }
            }

            yield return null;
        }
    }
}
