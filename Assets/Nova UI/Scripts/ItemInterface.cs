using UnityEngine;
using _Scripts.Systems.Item;

public class ItemInterface : MonoBehaviour
{

    [Tooltip("Item a ser adicionado ao inventário.")]
    public ItemBehaviour item;

    [Tooltip("Imagem a ser usaca como ícone do item.")]
    public Sprite icon;


    [Tooltip("Nome curto do item, usado na parte esquerda da loja.")]
    public string shortName;


    [Tooltip("Nome longo do item, usado junto a seu valor e descrição.")]
    public string longName;


    [Tooltip("Descrição do item. Não deve conter o que este cura.")]
    [TextAreaAttribute(7, 10)]
    public string description;


    [Tooltip("O que o item cura. Deve ser formatado de forma a ser inserido em um TextMeshPro.")]
    [TextAreaAttribute(7, 10)]
    public string cures;


    [Tooltip("Quanto custa esse item.")]
    public float value;

}
