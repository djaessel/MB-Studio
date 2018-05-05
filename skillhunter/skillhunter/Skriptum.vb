Public MustInherit Class Skriptum
    Public Enum ObjectType
        SCRIPT
        MISSION_TEMPLATE
        PRESENTATION
        GAME_MENU
        TROOP
        ITEM
        GAME_STRING
        SIMPLE_TRIGGER
        TRIGGER
        INFO_PAGE
        MESH
        MUSIC
        QUEST
        SOUND
        SCENE_PROP
        TABLEAU_MATERIAL
        MAP_ICON
        DIALOG
        FACTION
        ANIMATION
        PARTY_TEMPLATE
        PARTY
        SKILL
        POST_FX
        SKIN
        PARTICLE_SYSTEM
        SCENE
    End Enum

    Public Shared ReadOnly Prefixes As String() = {"script", "mt", "prsnt", "menu", "trp", "itm", "str", "simple_trigger", "trigger", "ip", "mesh", "track", "qst", "snd", "spr", "tableau", "icon",
                                                    "dialog", "fac", "anim", "pt", "p", "skl", "pfx", "skin", "psys", "scn"}

    Public ReadOnly Property ID As String
    Public ReadOnly Property ObjectTyp As ObjectType

    Public Sub New(sIdName As String, type As ObjectType)

        sIdName = sIdName.Trim()
        If sIdName.StartsWith(Prefix) Then
            sIdName = sIdName.Substring(Prefix.Length)
        End If

        ObjectTyp = type
        ID = sIdName
    End Sub

    Public ReadOnly Property Typ As Integer
        Get
            Return ObjectTyp
        End Get
    End Property

    Public ReadOnly Property Prefix As String
        Get
            Return Prefixes(ObjectTyp) + "_"c
        End Get
    End Property

End Class
