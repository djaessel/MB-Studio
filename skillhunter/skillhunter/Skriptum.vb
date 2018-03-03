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

    Private s_id_name As String
    Private type As ObjectType

    Public Sub New(s_id_name As String, type As ObjectType)
        Me.type = type
        s_id_name = s_id_name.Trim()
        If s_id_name.StartsWith(Prefix) Then
            s_id_name = s_id_name.Substring(Prefix.Length)
        End If
        Me.s_id_name = s_id_name
    End Sub

    Public ReadOnly Property ID As String
        Get
            Return s_id_name
        End Get
    End Property

    Public ReadOnly Property Typ As Integer
        Get
            Return type
        End Get
    End Property

    Public ReadOnly Property ObjectTyp As ObjectType
        Get
            Return type
        End Get
    End Property

    Public ReadOnly Property Prefix As String
        Get
            Return Prefixes(type) + "_"c
        End Get
    End Property

End Class
