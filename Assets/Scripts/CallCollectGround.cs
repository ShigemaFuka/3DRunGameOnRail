using UnityEngine;

/// <summary>
/// ƒJƒƒ‰‚Æ‘ÎÛ•¨‚ªˆê’è‹——£—£‚ê‚½‚ç(hit‚µ‚½‚ç)A”ñ•\¦‚Æ‚İ‚È‚µA‡‚ÉCollectŠÖ”‚ÅŠi”[‚·‚é
/// Œ©‚¦‚È‚¢‚Æ‚±‚ë‚ÅŠi”[
/// </summary>
public class CallCollectGround : MonoBehaviour
{
    [SerializeField] Vector3 _direction;
    [SerializeField] ObjectPoolGround _objectPoolGround;
    [SerializeField] float _length = 7;
    [Tooltip("Ši”[‚³‚ê‚½”")] int _count;

    void Start()
    {
        _count = 0;
    }

    /// <summary>
    /// ƒŒƒCƒLƒƒƒXƒg‚Å‚Ì“–‚½‚è”»’è‚Ì‚½‚ß‚Éü‚ğ•`‰æ
    /// “–‚½‚Á‚½‚çCollectŠÖ”‚ÅŠi”[ˆ—‚ğs‚¤
    /// </summary>
    void Update()
    {
        Ray ray = new Ray(gameObject.transform.position, _direction * _length);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.DrawRay(gameObject.transform.position, _direction * _length, Color.red);
            GameObject hitObj = hit.collider.gameObject;
            if (hitObj.activeSelf == true && hitObj.CompareTag("Ground"))
            {
                _objectPoolGround.Collect(hitObj);
                _count++;
            }
            //Ši”[”‚ª‚O‚Ì‚Æ‚«‚ÉLaunch‚µ‚æ‚¤‚Æ‚·‚é‚ÆANull‚ª•Ô‚Á‚Ä‚­‚é‚±‚Æ‚É‚æ‚èAˆ—‚É•s“s‡‚ª‚ ‚é‚½‚ß—]—T‚ğ‚½‚¹‚Ä‚¢‚é
            if (_count >= 3)
            {
                _objectPoolGround.Launch();
                _count--;
            }
        }
        Debug.DrawRay(gameObject.transform.position, _direction * _length, Color.red);
    }
}
