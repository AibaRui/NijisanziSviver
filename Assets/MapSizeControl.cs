using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSizeControl : MonoBehaviour
{

    [Header("�v���X�̕��̃}�b�v�̍��W")]
    [SerializeField] private Vector3 _upMapSize = default;

    [Header("�v���X�̕��̃}�b�v�̍��W")]
    [SerializeField] private Vector3 _minussMapSize = default;

    public Vector3 UpMapSize { get => _upMapSize; }

    public Vector3 MinussMapSize { get => _minussMapSize; }
}
