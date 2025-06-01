using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogEditorWindow : EditorWindow
{


    TextField ed;
    private Dialogue target;


    [MenuItem("Window/UI Toolkit/Dialog Editor Window")]
    public static void ShowExample()
    {
        DialogEditorWindow wnd = GetWindow<DialogEditorWindow>();
        wnd.titleContent = new GUIContent("Dialog Editor Window");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;
        VisualElement ve;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.

        ve = new Label("Dialogue");
        root.Add(ve);


        ed = new TextField();
        ed.multiline = true;
        ed.RegisterValueChangedCallback(evt =>
        {
            if (target)
            {
                if(evt.previousValue != evt.newValue)
                {
                    target.dialogText = evt.newValue;
                    UnityEditor.AssetDatabase.Refresh();
                }
            }

        });

        root.Add(ed);

        UnityEngine.UIElements.Button btn = new UnityEngine.UIElements.Button();
        btn.text = "extract";
        btn.clicked += Btn_onClick;
        root.Add(btn);


        OnSelectionChange();

    }

    private void Btn_onClick()
    {
        DialogManager dm = new DialogManager();
        dm.Extract();
    }

    void OnSelectionChange()
    {
        target = null;
        ed.value = "";

        if (Selection.activeObject is Dialogue)
        {
            target = Selection.activeObject as Dialogue;
            
        }
        if (Selection.activeObject is GameObject)
        {
            DialogTrigger dt = (Selection.activeObject as GameObject).GetComponent<DialogTrigger>();
            if (dt != null)
                target = dt.dialogue;
        }
        if (target)
        {
            ed.value = target.dialogText;
        }

    }

}
