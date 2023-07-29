using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class PlayerManager : MonoBehaviour, IPunObservable
{
    [SerializeField] MeshRenderer _mesh;

    [SerializeField] float _speed;
    PhotonView _view;
    Color _color;

    private void Start()
    {
        _view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (_view.IsMine)
        {
            Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            Vector3 newPos = transform.position + (moveInput * Time.deltaTime * _speed);
            transform.position = newPos;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _color = Random.ColorHSV();
                //ChangeColor(_color.r, _color.g, _color.b);
                //_view.RPC("ChangeColor", RpcTarget.Others, _color.r, _color.g, _color.b);
            }
        }

        _mesh.material.color = _color;
    }

    //[PunRPC]
    //private void ChangeColor(float r, float g, float b)
    //{
    //    _mesh.material.color = new Color(r, g, b);
    //}

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            float[] rgb = new float[] { _color.r, _color.g, _color.b };
            stream.SendNext(rgb);
        }
        else
        {
            float[] rgb = (float[])stream.ReceiveNext();
            _color = new Color(rgb[0], rgb[1], rgb[2]);
        }
    }
}
