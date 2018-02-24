# - by Johandros - #
#################################################################
# This file contains code replacements for better understanding #
# of the decompiled code in the new created *.py files          #
#################################################################

try_for_range > cur_x[X],0,0

try_for_range_backwards > cur_x[X],0,0

try_for_parties > cur_party

try_for_agents > cur_agent

try_for_prop_instances > cur_prop_instance,[scene_prop_id[X]]

try_for_players > cur_player,0

store_script_param_1 > param[X]

store_script_param_2 > param[X]

store_script_param > param[X],0 # 0 is script_param_no

entering_town > town_id[X]

set_player_troop > troop_id[X]

#multiplayer_send_message_to_server   = 388 # (multiplayer_send_int_to_server, <message_type>),

#multiplayer_send_int_to_server > 0,value

#multiplayer_send_2_int_to_server     = 390 # (multiplayer_send_2_int_to_server, <message_type>, <value>, <value>),

#multiplayer_send_3_int_to_server     = 391 # (multiplayer_send_3_int_to_server, <message_type>, <value>, <value>, <value>),

#multiplayer_send_4_int_to_server     = 392 # (multiplayer_send_4_int_to_server, <message_type>, <value>, <value>, <value>, <value>),

#multiplayer_send_string_to_server    = 393 # (multiplayer_send_string_to_server, <message_type>, <string_id>),

#multiplayer_send_message_to_player   = 394 # (multiplayer_send_message_to_player, <player_id>, <message_type>),

#multiplayer_send_int_to_player       = 395 # (multiplayer_send_int_to_player, <player_id>, <message_type>, <value>),

#multiplayer_send_2_int_to_player     = 396 # (multiplayer_send_2_int_to_player, <player_id>, <message_type>, <value>, <value>),

#multiplayer_send_3_int_to_player     = 397 # (multiplayer_send_3_int_to_player, <player_id>, <message_type>, <value>, <value>, <value>),

#multiplayer_send_4_int_to_player     = 398 # (multiplayer_send_4_int_to_player, <player_id>, <message_type>, <value>, <value>, <value>, <value>),

#multiplayer_send_string_to_player    = 399 # (multiplayer_send_string_to_player, <player_id>, <message_type>, <string_id>),

get_max_players > max_players

#player_is_active > player_id[X]

player_get_team_no > team_no[X],0 # 0 is player_id

#player_set_team_no                   = 403 # (player_set_team_no,  <player_id>, <team_no>),

player_get_troop_id > troop_id[X],0 # 0 is player_id

#player_set_troop_id                  = 405 # (player_get_troop_id, <destination>, <player_id>),

player_get_agent_id > agent_id[X],0 # 0 is player_id

player_get_gold > gold[X],0 # 0 is player_id

#player_set_gold                      = 408 # (player_set_gold, <player_id>, <value>, <max_value>), #set max_value to 0 if no limit is wanted

#player_spawn_new_agent               = 409 # (player_spawn_new_agent, <player_id>, <entry_point>),

#player_add_spawn_item                = 410 # (player_add_spawn_item, <player_id>, <item_slot_no>, <item_id>),

multiplayer_get_my_team > my_team_no

multiplayer_get_my_troop > my_troop_id

#multiplayer_set_my_troop             = 413 # (multiplayer_get_my_troop, <destination>),

multiplayer_get_my_gold > my_gold

multiplayer_get_my_player > my_player

#player_control_agent                 = 421 # (player_control_agent, <player_id>, <agent_id>),

player_get_item_id > item_id[X],0,0 #only for server -> 0 is player_id, 0 is for item_slot_no

player_get_banner_id > bannder_id[X],0 # 0 is player_id

#game_get_reduce_campaign_ai          = 424 # (game_get_reduce_campaign_ai, <destination>), #depreciated, use options_get_campaign_ai instead

multiplayer_find_spawn_point > spawn_point[X],0,0,0 #0 = team_no, 0 = examine_all_spawn_points, 0 = is_horseman

#set_spawn_effector_scene_prop_kind   = 426 # (set_spawn_effector_scene_prop_kind <team_no> <scene_prop_kind_no>)

#set_spawn_effector_scene_prop_id     = 427 # (set_spawn_effector_scene_prop_id <scene_prop_id>)

#player_set_is_admin                  = 429 # (player_set_is_admin, <player_id>, <value>), #value is 0 or 1

#player_is_admin                      = 430 # (player_is_admin, <player_id>),

player_get_score > score[X],0 # 0 is player_id]

#player_set_score                     = 432 # (player_set_score,<player_id>, <value>),

player_get_kill_count > kill_count[X],0 # 0 is player_id

#player_set_kill_count                = 434 # (player_set_kill_count,<player_id>, <value>),

player_get_death_count > death_count[X],0 # 0 is player_id

#player_set_death_count               = 436 # (player_set_death_count, <player_id>, <value>),

player_get_ping > ping[X],0 # 0 is player_id

#player_is_busy_with_menus            = 438 # (player_is_busy_with_menus, <player_id>),

player_get_is_muted > is_muted[X],0 # 0 is player_id

#player_set_is_muted                  = 440 # (player_set_is_muted, <player_id>, <value>, [mute_for_everyone]), #mute_for_everyone optional parameter should be set to 1 if player is muted for everyone (this works only on server).

player_get_unique_id > unique_id[X],0 # 0 is player_id #can only bew used on server side

player_get_gender > gender[X],0 # 0 is player_id

team_get_bot_kill_count > team_bot_kill_count[X],0 # 0 is team_id

#team_set_bot_kill_count              = 451 # (team_get_bot_kill_count, <destination>, <team_id>),

team_get_bot_death_count > team_bot_death_count[X],0 # 0 is team_id

#team_set_bot_death_count             = 453 # (team_get_bot_death_count, <destination>, <team_id>),

team_get_kill_count > team_kill_count[X],0 # 0 is team_id

team_get_score > team_score[X],0 # 0 is team_id

#team_set_score                       = 456 # (team_set_score, <team_id>, <value>),

#team_set_faction                     = 457 # (team_set_faction, <team_id>, <faction_id>),

team_get_faction > team_faction[X],0 # 0 is team_id

#player_save_picked_up_items_for_next_spawn  = 459 # (player_save_picked_up_items_for_next_spawn, <player_id>),

#player_get_value_of_original_items   = 460 # (player_get_value_of_original_items, <player_id>), #this operation returns values of the items, but default troop items will be counted as zero (except horse)

#player_item_slot_is_picked_up        = 461 # (player_item_slot_is_picked_up, <player_id>, <item_slot_no>), #item slots are overriden when player picks up an item and stays alive until the next round

#kick_player                          = 465 # (kick_player, <player_id>),

#ban_player                           = 466 # (ban_player, <player_id>, <value>, <player_id>), #set value = 1 for banning temporarily, assign 2nd player id as the administrator player id if banning is permanent

#save_ban_info_of_player              = 467 # (save_ban_info_of_player, <player_id>),

#ban_player_using_saved_ban_info      = 468 # (ban_player_using_saved_ban_info),

#start_multiplayer_mission            = 470 # (start_multiplayer_mission, <mission_template_id>, <scene_id>, <started_manually>),

#server_add_message_to_log            = 473 # (server_add_message_to_log, <string_id>),

server_get_renaming_server_allowed > is_renaming_server_allowed

server_get_changing_game_type_allowed > is_changing_game_type_allowed

##477 used for: server_set_anti_cheat                = 477 # (server_set_anti_cheat, <value>), #0 = off, 1 = on

server_get_combat_speed > server_combat_speed

#server_set_combat_speed              = 479 # (server_set_combat_speed, <value>), #0-2

server_get_friendly_fire > server_friendly_fire

#server_set_friendly_fire             = 481 # (server_set_friendly_fire, <value>), #0 = off, 1 = on

server_get_control_block_dir > server_control_block_direction

#server_set_control_block_dir         = 483 # (server_set_control_block_dir, <value>), #0 = automatic, 1 = by mouse movement

#server_set_password                  = 484 # (server_set_password, <string_id>),

server_get_add_to_game_servers_list > server_add_to_game_servers_list

#server_set_add_to_game_servers_list  = 486 # (server_set_add_to_game_servers_list, <value>),

server_get_ghost_mode > server_ghost_mode

#server_set_ghost_mode                = 488 # (server_set_ghost_mode, <value>),

#server_set_name                      = 489 # (server_set_name, <string_id>),

server_get_max_num_players > server_max_players

#server_set_max_num_players           = 491 # (server_set_max_num_players, <value>),

#server_set_welcome_message           = 492 # (server_set_welcome_message, <string_id>),

server_get_melee_friendly_fire > server_melee_friendly_fire

#server_set_melee_friendly_fire       = 494 # (server_set_melee_friendly_fire, <value>), #0 = off, 1 = on

server_get_friendly_fire_damage_self_ratio > server_friendly_fire_damage_self_ratio

#server_set_friendly_fire_damage_self_ratio   = 496 # (server_set_friendly_fire_damage_self_ratio, <value>), #0-100

server_get_friendly_fire_damage_friend_ratio > server_friendly_fire_damage_friend_ratio

#server_set_friendly_fire_damage_friend_ratio = 498 # (server_set_friendly_fire_damage_friend_ratio, <value>), #0-100

server_get_anti_cheat > server_anti_cheat

#server_set_anti_cheat                = 477 # (server_set_anti_cheat, <value>), #0 = off, 1 = on

## Set_slot operations. These assign a value to a slot. 
#troop_set_slot                  = 500 # (troop_set_slot,<troop_id>,<slot_no>,<value>),
#party_set_slot                  = 501 # (party_set_slot,<party_id>,<slot_no>,<value>),
#faction_set_slot                = 502 # (faction_set_slot,<faction_id>,<slot_no>,<value>),
#scene_set_slot                  = 503 # (scene_set_slot,<scene_id>,<slot_no>,<value>),
#party_template_set_slot         = 504 # (party_template_set_slot,<party_template_id>,<slot_no>,<value>),
#agent_set_slot                  = 505 # (agent_set_slot,<agent_id>,<slot_no>,<value>),
#quest_set_slot                  = 506 # (quest_set_slot,<quest_id>,<slot_no>,<value>),
#item_set_slot                   = 507 # (item_set_slot,<item_id>,<slot_no>,<value>),
#player_set_slot                 = 508 # (player_set_slot,<player_id>,<slot_no>,<value>),
#team_set_slot                   = 509 # (team_set_slot,<team_id>,<slot_no>,<value>),
#scene_prop_set_slot             = 510 # (scene_prop_set_slot,<scene_prop_instance_id>,<slot_no>,<value>),

## Get_slot operations. These retrieve the value of a slot. 
troop_get_slot > troop_slot[X],0,0 #0 = troop_id, 0 = slot_no

party_get_slot > party_slot[X],0,0 #0 = party_id, 0 = slot_no

faction_get_slot > faction_slot[X],0,0 #0 = faction_id, 0 = slot_no

scene_get_slot > scene_slot[X],0,0 #0 = scene_id, 0 = slot_no

party_template_get_slot > party_template_slot[X],0,0 #0 = party_template_id, 0 = slot_no

agent_get_slot > agent_slot[X],0,0 #0 = agent_id, 0 = slot_no

quest_get_slot > quest_slot[X],0,0 #0 = quest_id, 0 = slot_no

item_get_slot > item_slot[X],0,0 #0 = item_id, 0 = slot_no

player_get_slot > player_slot[X],0,0 #0 = player_id, 0 = slot_no

team_get_slot > team_slot[X],0,0 #0 = team_id, 0 = slot_no

scene_prop_get_slot > scene_prop_slot[X],0,0 #0 = scene_prop_instance_id, 0 = slot_no

## slot_eq operations. These check whether the value of a slot is equal to a given value.
#troop_slot_eq                   = 540 # (troop_slot_eq,<troop_id>,<slot_no>,<value>),
#party_slot_eq                   = 541 # (party_slot_eq,<party_id>,<slot_no>,<value>),
#faction_slot_eq                 = 542 # (faction_slot_eq,<faction_id>,<slot_no>,<value>),
#scene_slot_eq                   = 543 # (scene_slot_eq,<scene_id>,<slot_no>,<value>),
#party_template_slot_eq          = 544 # (party_template_slot_eq,<party_template_id>,<slot_no>,<value>),
#agent_slot_eq                   = 545 # (agent_slot_eq,<agent_id>,<slot_no>,<value>),
#quest_slot_eq                   = 546 # (quest_slot_eq,<quest_id>,<slot_no>,<value>),
#item_slot_eq                    = 547 # (item_slot_eq,<item_id>,<slot_no>,<value>),
#player_slot_eq                  = 548 # (player_slot_eq,<player_id>,<slot_no>,<value>),
#team_slot_eq                    = 549 # (team_slot_eq,<team_id>,<slot_no>,<value>),
#scene_prop_slot_eq              = 550 # (scene_prop_slot_eq,<scene_prop_instance_id>,<slot_no>,<value>),

## slot_ge operations. These check whether the value of a slot is greater than or equal to a given value.
#troop_slot_ge                   = 560 # (troop_slot_ge,<troop_id>,<slot_no>,<value>),
#party_slot_ge                   = 561 # (party_slot_ge,<party_id>,<slot_no>,<value>),
#faction_slot_ge                 = 562 # (faction_slot_ge,<faction_id>,<slot_no>,<value>),
#scene_slot_ge                   = 563 # (scene_slot_ge,<scene_id>,<slot_no>,<value>),
#party_template_slot_ge          = 564 # (party_template_slot_ge,<party_template_id>,<slot_no>,<value>),
#agent_slot_ge                   = 565 # (agent_slot_ge,<agent_id>,<slot_no>,<value>),
#quest_slot_ge                   = 566 # (quest_slot_ge,<quest_id>,<slot_no>,<value>),
#item_slot_ge                    = 567 # (item_slot_ge,<item_id>,<slot_no>,<value>),
#player_slot_ge                  = 568 # (player_slot_ge,<player_id>,<slot_no>,<value>),
#team_slot_ge                    = 569 # (team_slot_ge,<team_id>,<slot_no>,<value>),
#scene_prop_slot_ge              = 570 # (scene_prop_slot_ge,<scene_prop_instance_id>,<slot_no>,<value>),

#play_sound_at_position          = 599 # (play_sound_at_position, <sound_id>, <position_no>, [options]),
#play_sound                      = 600 # (play_sound,<sound_id>,[options]),
#play_track                      = 601 # (play_track,<track_id>, [options]), # 0 = default, 1 = fade out current track, 2 = stop current track
#play_cue_track                  = 602 # (play_cue_track,<track_id>), #starts immediately
#music_set_situation             = 603 # (music_set_situation, <situation_type>),
#music_set_culture               = 604 # (music_set_culture, <culture_type>),
#stop_all_sounds                 = 609 # (stop_all_sounds, [options]), # 0 = stop only looping sounds, 1 = stop all sounds
#store_last_sound_channel        = 615 # (store_last_sound_channel, <destination>),
#stop_sound_channel              = 616 # (stop_sound_channel, <sound_channel_no>),

get_angle_between_positions > angle[X],0,0 # 0 = pos1, 0 = pos2

get_distance_between_positions > distance[X],0,0 # 0 = pos1, 0 = pos2 # in centimeters

get_distance_between_positions_in_meters > distance[X],0,0 # 0 = pos1, 0 = pos2 # in meters

get_sq_distance_between_positions > sq_distance[X],0,0 # 0 = pos1, 0 = pos2 # in centimeters

get_sq_distance_between_positions_in_meters > sq_distance[X],0,0 # 0 = pos1, 0 = pos2 # in meters

get_sq_distance_between_position_heights > sq_distance[X],0,0 # 0 = pos1, 0 = pos2 # in centimeters

#position_transform_position_to_parent       = 716 # (position_transform_position_to_parent,<dest_position_no>,<position_no>,<position_no_to_be_transformed>),
#position_transform_position_to_local        = 717 # (position_transform_position_to_local, <dest_position_no>,<position_no>,<position_no_to_be_transformed>),

#position_move_x                 = 720 # movement is in cms, [0 = local; 1=global] # (position_move_x,<position_no>,<movement>,[value]),
#position_move_y                 = 721 # (position_move_y,<position_no>,<movement>,[value]),
#position_move_z                 = 722 # (position_move_z,<position_no>,<movement>,[value]),

#position_rotate_x               = 723 # (position_rotate_x,<position_no>,<angle>),
#position_rotate_y               = 724 # (position_rotate_y,<position_no>,<angle>),
#position_rotate_z               = 725 # (position_rotate_z,<position_no>,<angle>,[use_global_z_axis]), # set use_global_z_axis as 1 if needed, otherwise you don't have to give that.

position_get_x > pos_x[X],0 # 0 = pos_no

position_get_y > pos_y[X],0 # 0 = pos_no

position_get_z > pos_z[X],0 # 0 = pos_no

#position_set_x                  = 729 # (position_set_x,<position_no>,<value_fixed_point>), #meters / fixed point multiplier is set
#position_set_y                  = 730 # (position_set_y,<position_no>,<value_fixed_point>), #meters / fixed point multiplier is set
#position_set_z                  = 731 # (position_set_z,<position_no>,<value_fixed_point>), #meters / fixed point multiplier is set

position_get_scale_x > pos_scale_x[X],0 # 0 = pos_no

position_get_scale_y > pos_scale_y[X],0 # 0 = pos_no

position_get_scale_z > pos_scale_z[X],0 # 0 = pos_no

#position_rotate_x_floating      = 738 # (position_rotate_x_floating,<position_no>,<angle>), #angle in degree * fixed point multiplier 
#position_rotate_y_floating      = 739 # (position_rotate_y_floating,<position_no>,<angle>), #angle in degree * fixed point multiplier 
#position_rotate_z_floating      = 734 # (position_rotate_z_floating,<position_no>,<angle>), #angle in degree * fixed point multiplier 

position_get_rotation_around_z > rotation_around_z[X],0 # 0 = pos_no

#position_normalize_origin       = 741 # (position_normalize_origin,<destination_fixed_point>,<position_no>),
#                                                                      # destination = convert_to_fixed_point(length(position.origin))
#                                                                      # position.origin *= 1/length(position.origin) #so it normalizes the origin vector

position_get_rotation_around_x > rotation_around_x[X],0 # 0 = pos_no
position_get_rotation_around_y > rotation_around_y[X],0 # 0 = pos_no

#position_set_scale_x            = 744 # (position_set_scale_x, <position_no>, <value_fixed_point>), #x scale in meters / fixed point multiplier is set
#position_set_scale_y            = 745 # (position_set_scale_y, <position_no>, <value_fixed_point>), #y scale in meters / fixed point multiplier is set
#position_set_scale_z            = 746 # (position_set_scale_z, <position_no>, <value_fixed_point>), #z scale in meters / fixed point multiplier is set

position_get_distance_to_terrain > distance_to_terrain[X],0 # 0 = pos_no

position_get_distance_to_ground_level > distance_to_ground_level[X],0 # 0 = pos_no

#start_presentation                        = 900 # (start_presentation, <presentation_id>),
#start_background_presentation             = 901 # (start_background_presentation, <presentation_id>), #can only be used in game menus
#presentation_set_duration                 = 902 # (presentation_set_duration, <duration-in-1/100-seconds>), #there must be an active presentation
#is_presentation_active                    = 903 # (is_presentation_active, <presentation_id),

create_text_overlay > text_overlay[X],0 # 0 = string_id

create_mesh_overlay > mesh_overlay[X],0 # 0 = mesh_id

create_button_overlay > button_overlay[X],0 # 0 = string_id

create_image_button_overlay > image_button_overlay[X],0,0 # 0 = mesh_id, 0 = mesh_id # 2. mesh is pressed button

create_slider_overlay > slider_overlay[X],0,0 # 0 = min_value, 0 = max_value

create_progress_overlay > progress_overlay[X],0,0 # 0 = min_value, 0 = max_value

create_combo_button_overlay > combo_button_overlay[X]

create_text_box_overlay > text_box_overlay[X]

create_check_box_overlay > check_box_overlay[X]

create_simple_text_box_overlay > simple_text_box_overlay[X]

#overlay_set_text                          = 920 # (overlay_set_text, <overlay_id>, <string_id>),
#overlay_set_color                         = 921 # (overlay_set_color, <overlay_id>, <color>), #color in RGB format like 0xRRGGBB (put hexadecimal values for RR GG and BB parts)
#overlay_set_alpha                         = 922 # (overlay_set_alpha, <overlay_id>, <alpha>), #alpha in A format like 0xAA (put hexadecimal values for AA part)
#overlay_set_hilight_color                 = 923 # (overlay_set_hilight_color, <overlay_id>, <color>), #color in RGB format like 0xRRGGBB (put hexadecimal values for RR GG and BB parts)
#overlay_set_hilight_alpha                 = 924 # (overlay_set_hilight_alpha, <overlay_id>, <alpha>), #alpha in A format like 0xAA (put hexadecimal values for AA part)
#overlay_set_size                          = 925 # (overlay_set_size, <overlay_id>, <position_no>), #position's x and y values are used
#overlay_set_position                      = 926 # (overlay_set_position, <overlay_id>, <position_no>), #position's x and y values are used
#overlay_set_val                           = 927 # (overlay_set_val, <overlay_id>, <value>), #can be used for sliders, combo buttons and check boxes
#overlay_set_boundaries                    = 928 # (overlay_set_boundaries, <overlay_id>, <min_value>, <max_value>),
#overlay_set_area_size                     = 929 # (overlay_set_area_size, <overlay_id>, <position_no>), #position's x and y values are used
#overlay_set_mesh_rotation                 = 930 # (overlay_set_mesh_rotation, <overlay_id>, <position_no>), #position's rotation values are used for rotations around x, y and z axis
#overlay_add_item                          = 931 # (overlay_add_item, <overlay_id>, <string_id>), # adds an item to the combo box
#overlay_animate_to_color                  = 932 # (overlay_animate_to_color, <overlay_id>, <duration-in-1/1000-seconds>, <color>), #alpha value will not be used
#overlay_animate_to_alpha                  = 933 # (overlay_animate_to_alpha, <overlay_id>, <duration-in-1/1000-seconds>, <color>), #only alpha value will be used
#overlay_animate_to_highlight_color        = 934 # (overlay_animate_to_highlight_color, <overlay_id>, <duration-in-1/1000-seconds>, <color>), #alpha value will not be used
#overlay_animate_to_highlight_alpha        = 935 # (overlay_animate_to_highlight_alpha, <overlay_id>, <duration-in-1/1000-seconds>, <color>), #only alpha value will be used
#overlay_animate_to_size                   = 936 # (overlay_animate_to_size, <overlay_id>, <duration-in-1/1000-seconds>, <position_no>), #position's x and y values are used as
#overlay_animate_to_position               = 937 # (overlay_animate_to_position, <overlay_id>, <duration-in-1/1000-seconds>, <position_no>), #position's x and y values are used as
#create_image_button_overlay_with_tableau_material = 938 # (create_image_button_overlay_with_tableau_material, <destination>, <mesh_id>, <tableau_material_id>, <value>), #returns overlay id. value is passed to tableau_material
#                                                        # when mesh_id is -1, a default mesh is generated automatically
#create_mesh_overlay_with_tableau_material = 939 # (create_mesh_overlay_with_tableau_material, <destination>, <mesh_id>, <tableau_material_id>, <value>), #returns overlay id. value is passed to tableau_material
#                                                        # when mesh_id is -1, a default mesh is generated automatically

create_game_button_overlay > game_button_overlay[X],0 # 0 = string_id

create_in_game_button_overlay > in_game_button_overlay[X],0 # 0 = string_id

create_number_box_overlay > number_box_overlay[X],0,0 # 0 = min_value, 0 = max_value

create_listbox_overlay > listbox_overlay[X]

create_mesh_overlay_with_item_id > mesh_overlay_with_item[X],0 # 0 = item_id

#set_container_overlay                     = 945 # (set_container_overlay, <overlay_id>), #sets the container overlay that new overlays will attach to. give -1 to reset

#overlay_get_position                      = 946 # (overlay_get_position, <destination>, <overlay_id>),

#overlay_set_display                       = 947 # (overlay_set_display, <overlay_id>, <value>), #shows/hides overlay (1 = show, 0 = hide)

create_combo_label_overlay > combo_label_overlay[X]

#overlay_obtain_focus                      = 949 # (overlay_obtain_focus, <overlay_id>), #works for textboxes only

#overlay_set_tooltip                       = 950 # (overlay_set_tooltip, <overlay_id>, <string_id>),
#overlay_set_container_overlay             = 951 # (overlay_set_container_overlay, <overlay_id>, <container_overlay_id>), # -1 to reset
#overlay_set_additional_render_height      = 952 # (overlay_set_additional_render_height, <overlay_id>, <height_adder>),
#overlay_set_material                      = 956 # (overlay_set_material, <overlay_id>, <string_no>),

#show_object_details_overlay               = 960 # (show_object_details_overlay, <value>), #0 = hide, 1 = show

#show_item_details                         = 970 # (show_item_details, <item_id>, <position_no>, <price_multiplier>), #price_multiplier is percent, usually returned by script_game_get_item_[buy/sell]_price_factor
#close_item_details                        = 971 # (close_item_details),
#show_item_details_with_modifier           = 972 # (show_item_details_with_modifier, <item_id>, <item_modifier>, <position_no>, <price_multiplier>), #price_multiplier is percent, usually returned by script_game_get_item_[buy/sell]_price_factor

#context_menu_add_item                     = 980 # (right_mouse_menu_add_item, <string_id>, <value>), #must be called only inside script_game_right_mouse_menu_get_buttons

get_average_game_difficulty > average_game_difficulty

get_level_boundary > level_boundary[X],0 # 0 = level_no

#set_camera_follow_party         = 1021 # (set_camera_follow_party,<party_id>), #Works on map only.
#start_map_conversation          = 1025 # (start_map_conversation,<troop_id>),
#rest_for_hours                  = 1030 # (rest_for_hours,<rest_period>,[time_speed],[remain_attackable]),
#rest_for_hours_interactive      = 1031 # (rest_for_hours_interactive,<rest_period>,[time_speed],[remain_attackable]),

#add_xp_to_troop                 = 1062 # (add_xp_to_troop,<value>,[troop_id]),
#add_gold_as_xp                  = 1063 # (add_gold_as_xp,<value>,[troop_id]),
#add_xp_as_reward                = 1064 # (add_xp_as_reward,<value>),

#add_gold_to_party               = 1070 # party_id should be different from 0
#			       # (add_gold_to_party,<value>,<party_id>),

#set_party_creation_random_limits= 1080 # (set_party_creation_random_limits, <min_value>, <max_value>), (values should be between 0, 100)

#troop_set_note_available        = 1095 # (troop_set_note_available, <troop_id>, <value>), #1 = available, 0 = not available
#faction_set_note_available      = 1096 # (faction_set_note_available, <faction_id>, <value>), #1 = available, 0 = not available
#party_set_note_available        = 1097 # (party_set_note_available, <party_id>, <value>), #1 = available, 0 = not available
#quest_set_note_available        = 1098 # (quest_set_note_available, <quest_id>, <value>), #1 = available, 0 = not available

#1090-1091-1092 is taken, see below (info_page)
#spawn_around_party              = 1100 # ID of spawned party is put into reg(0)
#			       # (spawn_around_party,<party_id>,<party_template_id>),
#set_spawn_radius                = 1103 # (set_spawn_radius,<value>),

#display_debug_message           = 1104 # (display_debug_message,<string_id>,[hex_colour_code]), #displays message only in debug mode, but writes to rgl_log.txt in both release and debug modes when edit mode is enabled
#display_log_message             = 1105 # (display_log_message,<string_id>,[hex_colour_code]),
#display_message                 = 1106 # (display_message,<string_id>,[hex_colour_code]),
#set_show_messages               = 1107 # (set_show_messages,<value>), #0 disables window messages 1 re-enables them.

#add_troop_note_tableau_mesh     = 1108 # (add_troop_note_tableau_mesh,<troop_id>,<tableau_material_id>),
#add_faction_note_tableau_mesh   = 1109 # (add_faction_note_tableau_mesh,<faction_id>,<tableau_material_id>),
#add_party_note_tableau_mesh     = 1110 # (add_party_note_tableau_mesh,<party_id>,<tableau_material_id>),
#add_quest_note_tableau_mesh     = 1111 # (add_quest_note_tableau_mesh,<quest_id>,<tableau_material_id>),
#add_info_page_note_tableau_mesh = 1090 # (add_info_page_note_tableau_mesh,<info_page_id>,<tableau_material_id>),
#add_troop_note_from_dialog      = 1114 # (add_troop_note_from_dialog,<troop_id>,<note_slot_no>, <value>), #There are maximum of 8 slots. value = 1 -> shows when the note is added
#add_faction_note_from_dialog    = 1115 # (add_faction_note_from_dialog,<faction_id>,<note_slot_no>, <value>), #There are maximum of 8 slots value = 1 -> shows when the note is added
#add_party_note_from_dialog      = 1116 # (add_party_note_from_dialog,<party_id>,<note_slot_no>, <value>), #There are maximum of 8 slots value = 1 -> shows when the note is added
#add_quest_note_from_dialog      = 1112 # (add_quest_note_from_dialog,<quest_id>,<note_slot_no>, <value>), #There are maximum of 8 slots value = 1 -> shows when the note is added
#add_info_page_note_from_dialog  = 1091 # (add_info_page_note_from_dialog,<info_page_id>,<note_slot_no>, <value>), #There are maximum of 8 slots value = 1 -> shows when the note is added
#add_troop_note_from_sreg        = 1117 # (add_troop_note_from_sreg,<troop_id>,<note_slot_no>,<string_id>, <value>), #There are maximum of 8 slots value = 1 -> shows when the note is added
#add_faction_note_from_sreg      = 1118 # (add_faction_note_from_sreg,<faction_id>,<note_slot_no>,<string_id>, <value>), #There are maximum of 8 slots value = 1 -> shows when the note is added
#add_party_note_from_sreg        = 1119 # (add_party_note_from_sreg,<party_id>,<note_slot_no>,<string_id>, <value>), #There are maximum of 8 slots value = 1 -> shows when the note is added
#add_quest_note_from_sreg        = 1113 # (add_quest_note_from_sreg,<quest_id>,<note_slot_no>,<string_id>, <value>), #There are maximum of 8 slots value = 1 -> shows when the note is added
#add_info_page_note_from_sreg    = 1092 # (add_info_page_note_from_sreg,<info_page_id>,<note_slot_no>,<string_id>, <value>), #There are maximum of 8 slots value = 1 -> shows when the note is added

#tutorial_box                    = 1120 # (tutorial_box,<string_id>,<string_id>), #deprecated use dialog_box instead.
#dialog_box                      = 1120 # (dialog_box,<text_string_id>,<title_string_id>),
#question_box                    = 1121 # (question_box,<string_id>, [<yes_string_id>], [<no_string_id>]),
#tutorial_message                = 1122 # (tutorial_message,<string_id>, <color>, <auto_close_time>), #set string_id = -1 for hiding the message
#tutorial_message_set_position   = 1123 # (tutorial_message_set_position, <position_x>, <position_y>), 
#tutorial_message_set_size       = 1124 # (tutorial_message_set_size, <size_x>, <size_y>), 
#tutorial_message_set_center_justify = 1125 # (tutorial_message_set_center_justify, <val>), #set not 0 for center justify, 0 for not center justify
#tutorial_message_set_background = 1126 # (tutorial_message_set_background, <value>), #1 = on, 0 = off, default is off

#set_tooltip_text                = 1130 #  (set_tooltip_text, <string_id>),

#set_price_rate_for_item         = 1171 # (set_price_rate_for_item,<item_id>,<value_percentage>),
#set_price_rate_for_item_type    = 1172 # (set_price_rate_for_item_type,<item_type_id>,<value_percentage>),

#party_join                      = 1201 # (party_join),
#party_join_as_prisoner          = 1202 # (party_join_as_prisoner),
#troop_join                      = 1203 # (troop_join,<troop_id>),
#troop_join_as_prisoner          = 1204 # (troop_join_as_prisoner,<troop_id>),

#remove_member_from_party        = 1210 # (remove_member_from_party,<troop_id>,[party_id]),
#remove_regular_prisoners        = 1211 # (remove_regular_prisoners,<party_id>),
#remove_troops_from_companions   = 1215 # (remove_troops_from_companions,<troop_id>,<value>),
#remove_troops_from_prisoners    = 1216 # (remove_troops_from_prisoners,<troop_id>,<value>),

#heal_party                      = 1225 # (heal_party,<party_id>),

#disable_party                   = 1230 # (disable_party,<party_id>),
#enable_party                    = 1231 # (enable_party,<party_id>),
#remove_party                    = 1232 # (remove_party,<party_id>), #remove only active spawned parties or you will corrupt the game! Non-spawned parties may be disabled...
#add_companion_party             = 1233 # (add_companion_party,<troop_id_hero>),

#add_troop_to_site               = 1250 # (add_troop_to_site,<troop_id>,<scene_id>,<entry_no>),
#remove_troop_from_site          = 1251 # (remove_troop_from_site,<troop_id>,<scene_id>),
#modify_visitors_at_site         = 1261 # (modify_visitors_at_site,<scene_id>),

#set_visitor                     = 1263 # (set_visitor,<entry_no>,<troop_id>,[<dna>]),
#set_visitors                    = 1264 # (set_visitors,<entry_no>,<troop_id>,<number_of_troops>),
#add_visitors_to_current_scene   = 1265 # (add_visitors_to_current_scene,<entry_no>,<troop_id>,<number_of_troops>, <team_no>, <group_no>), #team no and group no are used in multiplayer mode only. default team in entry is used in single player mode
#scene_set_day_time              = 1266 # (scene_set_day_time, <value>), #value in hours (0-23), must be called within ti_before_mission_start triggers

#set_relation                    = 1270 # (set_relation,<faction_id>,<faction_id>,<value>),
#faction_set_name                = 1275 # (faction_set_name, <faction_id>, <string_id>),
#faction_set_color               = 1276 # (faction_set_color, <faction_id>, <value>),

faction_get_color > faction_color[X],0 # 0 = faction_id

#Quest stuff
#start_quest              = 1280 # (start_quest,<quest_id>),
#complete_quest           = 1281 # (complete_quest,<quest_id>),
#succeed_quest            = 1282 # (succeed_quest,<quest_id>), #also concludes the quest
#fail_quest               = 1283 # (fail_quest,<quest_id>), #also concludes the quest
#cancel_quest             = 1284 # (cancel_quest,<quest_id>),

#set_quest_progression    = 1285 # (set_quest_progression,<quest_id>,<value>),

#conclude_quest           = 1286 # (conclude_quest,<quest_id>),

#setup_quest_text         = 1290 # (setup_quest_text,<quest_id>),
#setup_quest_giver        = 1291 # (setup_quest_giver,<quest_id>, <string_id>),

#encounter outcomes.
#start_encounter            = 1300 # (start_encounter,<party_id>),

#select_enemy               = 1303 # (select_enemy,<value>),
#set_passage_menu           = 1304 # (set_passage_menu,<value>),

#set_mercenary_source_party = 1320 # selects party from which to buy mercenaries
#				   # (set_mercenary_source_party,<party_id>),

#set_merchandise_modifier_quality = 1490	        # Quality rate in percentage (average quality = 100),
#						# (set_merchandise_modifier_quality,<value>),
#set_merchandise_max_value = 1491		# (set_merchandise_max_value,<value>),

#set_item_probability_in_merchandise = 1493	# (set_item_probability_in_merchandise,<itm_id>,<value>),

#active Troop
#set_active_troop                       = 1050
#troop_set_name                         = 1501   # (troop_set_name, <troop_id>, <string_no>),
#troop_set_plural_name                  = 1502   # (troop_set_plural_name, <troop_id>, <string_no>),
#troop_set_face_key_from_current_profile= 1503   # (troop_set_face_key_from_current_profile, <troop_id>),
#troop_set_type                         = 1505	# (troop_set_type,<troop_id>,<gender>),

troop_get_type > troop_type[X],0 # 0 = troop_id

#troop_is_hero                          = 1507   # (troop_is_hero,<troop_id>),
#troop_is_wounded                       = 1508   # (troop_is_wounded,<troop_id>), #only for heroes!
#troop_set_auto_equip                   = 1509   # (troop_set_auto_equip,<troop_id>,<value>),#disables otr enables auto-equipping
#troop_ensure_inventory_space           = 1510	# (troop_ensure_inventory_space,<troop_id>,<value>),
#troop_sort_inventory                   = 1511	# (troop_sort_inventory,<troop_id>),
#troop_add_merchandise                  = 1512	# (troop_add_merchandise,<troop_id>,<item_type_id>,<value>),
#troop_add_merchandise_with_faction     = 1513	# (troop_add_merchandise_with_faction,<troop_id>,<faction_id>,<item_type_id>,<value>), #faction_id is given to check if troop is eligible to produce that item

troop_get_xp > troop_xp[X],0 # 0 = troop_id

troop_get_class > troop_class[X],0 # 0 = troop_id

#troop_set_class                        = 1517 # (troop_set_class, <troop_id>, <value>),

#troop_raise_attribute                  = 1520	# (troop_raise_attribute,<troop_id>,<attribute_id>,<value>),
#troop_raise_skill                      = 1521	# (troop_raise_skill,<troop_id>,<skill_id>,<value>),
#troop_raise_proficiency                = 1522	# (troop_raise_proficiency,<troop_id>,<proficiency_no>,<value>),
#troop_raise_proficiency_linear         = 1523	# raises weapon proficiencies linearly without being limited by weapon master skill
#						# (troop_raise_proficiency,<troop_id>,<proficiency_no>,<value>),

#troop_add_proficiency_points           = 1525   # (troop_add_proficiency_points,<troop_id>,<value>),					
#troop_add_gold                         = 1528	# (troop_add_gold,<troop_id>,<value>),
#troop_remove_gold                      = 1529	# (troop_remove_gold,<troop_id>,<value>),
#troop_add_item                         = 1530	# (troop_add_item,<troop_id>,<item_id>,[modifier]),
#troop_remove_item                      = 1531	# (troop_remove_item,<troop_id>,<item_id>),
#troop_clear_inventory                  = 1532	# (troop_clear_inventory,<troop_id>),
#troop_equip_items                      = 1533   # (troop_equip_items,<troop_id>), #equips the items in the inventory automatically
#troop_inventory_slot_set_item_amount   = 1534   # (troop_inventory_slot_set_item_amount,<troop_id>,<inventory_slot_no>,<value>),

troop_inventory_slot_get_item_amount > troop_inv_slot_item_amount[X],0,0 # 0 = troop_id, 0 = inventory_slot_no

troop_inventory_slot_get_item_max_amount > troop_inv_slot_item_max_amount[X],0,0 # 0 = troop_id, 0 = inventory_slot_no

#troop_add_items                        = 1535	# (troop_add_items,<troop_id>,<item_id>,<number>),
#troop_remove_items                     = 1536	# puts cost of items to reg0
#                                                # (troop_remove_items,<troop_id>,<item_id>,<number>),
#troop_loot_troop                       = 1539	# (troop_loot_troop,<target_troop>,<source_troop_id>,<probability>), 

troop_get_inventory_capacity > troop_inv_capacity[X],0 # 0 = troop_id

troop_get_inventory_slot > troop_inv_slot[X],0,0 # 0 = troop_id, 0 = inventory_slot_no

troop_get_inventory_slot_modifier > troop_inv_slot_modifier[X],0,0 # 0 = troop_id, 0 = inventory_slot_no

#troop_set_inventory_slot               = 1543	# (troop_set_inventory_slot,<troop_id>,<inventory_slot_no>,<value>),
#troop_set_inventory_slot_modifier      = 1544	# (troop_set_inventory_slot_modifier,<troop_id>,<inventory_slot_no>,<value>),
#troop_set_faction                      = 1550 # (troop_set_faction,<troop_id>,<faction_id>),
#troop_set_age                          = 1555 # (troop_set_age, <troop_id>, <age_slider_pos>),  #Enter a value between 0..100
#troop_set_health                       = 1560	# (troop_set_health,<troop_id>,<relative health (0-100)>),

troop_get_upgrade_troop > troop_upgrade_troop_id[X],0,0 # 0 = troop_id, 0 = upgrade_path -> 0 - 1. node; 1 - 2. node; returns -1 = unavailable

#Items...
item_get_type > item_type,0 # 0 = item_id

#Parties...
party_get_num_companions > party_num_companions[X],0 # 0 = party_id

party_get_num_prisoners > party_num_prisioners[X],0 # 0 = party_id

#party_set_flags                        = 1603   # (party_set_flag, <party_id>, <flag>, <clear_or_set>), #sets flags like pf_default_behavior. see header_parties.py for flags.
#party_set_marshall                     = 1604   # (party_set_marshal, <party_id>, <value>)
#party_set_extra_text                   = 1605   # (party_set_extra_text,<party_id>, <string>)
#party_set_aggressiveness               = 1606   # (party_set_aggressiveness, <party_id>, <number>),
#party_set_courage                      = 1607   # (party_set_courage, <party_id>, <number>),

party_get_current_terrain > party_cur_terrain[X],0 # 0 = party_id

party_get_template_id > party_template_id[X],0 # 0 = party_id

#party_add_members                      = 1610	# (party_add_members,<party_id>,<troop_id>,<number>), #returns number added in reg0
#party_add_prisoners                    = 1611	# (party_add_prisoners,<party_id>,<troop_id>,<number>),#returns number added in reg0
#party_add_leader                       = 1612	# (party_add_leader,<party_id>,<troop_id>,[<number>]),
#party_force_add_members                = 1613	# (party_force_add_members,<party_id>,<troop_id>,<number>),
#party_force_add_prisoners              = 1614	# (party_force_add_prisoners,<party_id>,<troop_id>,<number>),

#party_remove_members                   = 1615	# stores number removed to reg0
#						# (party_remove_members,<party_id>,<troop_id>,<number>),
#party_remove_prisoners                 = 1616	# stores number removed to reg0
#						# (party_remove_members,<party_id>,<troop_id>,<number>),
#party_clear                            = 1617	# (party_clear,<party_id>),
#party_wound_members                    = 1618	# (party_wound_members,<party_id>,<troop_id>,<number>),
#party_remove_members_wounded_first     = 1619	# stores number removed to reg0
#						# (party_remove_members_wounded_first,<party_id>,<troop_id>,<number>),

#party_set_faction                      = 1620	# (party_set_faction,<party_id>,<faction_id>),
#party_relocate_near_party              = 1623	# (party_relocate_near_party,<party_id>,<target_party_id>,<value_spawn_radius>),

#party_get_position                     = 1625	# (party_get_position,<position_no>,<party_id>),

#party_set_position                     = 1626	# (party_set_position,<party_id>,<position_no>),
#map_get_random_position_around_position= 1627	# (map_get_random_position_around_position,<dest_position_no>,<source_position_no>,<radius>),
#map_get_land_position_around_position  = 1628	# (map_get_land_position_around_position,<dest_position_no>,<source_position_no>,<radius>),
#map_get_water_position_around_position = 1629	# (map_get_water_position_around_position,<dest_position_no>,<source_position_no>,<radius>),

party_count_members_of_type > party_members_of_type[X],0,0 # 0 = party_id, 0 = troop_id

party_count_companions_of_type > party_companions_of_type[X],0,0 # 0 = party_id, 0 = troop_id

party_count_prisoners_of_type > party_prisoners_of_type[X],0,0 # 0 = party_id, 0 = troop_id

party_get_free_companions_capacity > party_free_companions_capacity[X],0 # 0 = party_id

party_get_free_prisoners_capacity > party_free_prisoners_capacity[X],0 # 0 = party_id

party_get_ai_initiative > party_ai_initiative[X],0 # 0 = party_id

#party_set_ai_initiative                = 1639	# (party_set_ai_initiative,<party_id>,<value>), #value is between 0-100
#party_set_ai_behavior                  = 1640	# (party_set_ai_behavior,<party_id>,<ai_bhvr>),
#party_set_ai_object                    = 1641	# (party_set_ai_object,<party_id>,<party_id>),
#party_set_ai_target_position           = 1642	# (party_set_ai_target_position,<party_id>,<position_no>),
#party_set_ai_patrol_radius             = 1643	# (party_set_ai_patrol_radius,<party_id>,<radius_in_km>),
#party_ignore_player                    = 1644   # (party_ignore_player, <party_id>,<duration_in_hours>), #don't pursue player party for this duration
#party_set_bandit_attraction            = 1645   # (party_set_bandit_attraction, <party_id>,<attaraction>), #set how attractive a target the party is for bandits (0..100)

party_get_helpfulness > party_helpfulness[X],0 # 0 = party_id

#party_set_helpfulness                  = 1647   # (party_set_helpfulness, <party_id>, <number>), #tendency to help friendly parties under attack. (0-10000, 100 default.)
#party_set_ignore_with_player_party     = 1648   # (party_set_ignore_with_player_party, <party_id>, <value>),

#party_get_ignore_with_player_party     = 1649   # (party_get_ignore_with_player_party, <party_id>),

party_get_num_companion_stacks > party_num_companions_stacks[X],0 # 0 = party_id

party_get_num_prisoner_stacks > party_num_prisoners_stacks[X],0 # 0 = party_id

party_stack_get_troop_id > troop_id[X],0,0 # 0 = party_id, 0 = stack_no

party_stack_get_size > party_stack_size[X],0,0 # 0 = party_id, 0 = stack_no

party_stack_get_num_wounded > party_stack_num_wounded[X],0,0 # 0 = party_id, 0 = stack_no

party_stack_get_troop_dna > party_stack_troop_dna[X],0,0 # 0 = party_id, 0 = stack_no

party_prisoner_stack_get_troop_id > party_prisoner_troop_id[X],0,0 # 0 = party_id, 0 = stack_no

party_prisoner_stack_get_size > party_prisoner_stack_size[X],0,0 # 0 = party_id, 0 = stack_no

party_prisoner_stack_get_troop_dna > party_prisoner_stack_troop_dna[X],0,0 # 0 = party_id, 0 = stack_no

#party_attach_to_party                  = 1660   # (party_attach_to_party, <party_id>, <party_id to attach to>),
#party_detach                           = 1661   # (party_detach, <party_id>),
#party_collect_attachments_to_party     = 1662   # (party_collect_attachments_to_party, <party_id>, <destination party_id>),
#party_quick_attach_to_current_battle   = 1663   # (party_quick_attach_to_current_battle, <party_id>, <side (0:players side, 1:enemy side)>),

party_get_cur_town > party_cur_town[X],0 # 0 = party_id

#party_leave_cur_battle                 = 1666   # (party_leave_cur_battle, <party_id>),
#party_set_next_battle_simulation_time  = 1667   # (party_set_next_battle_simulation_time,<party_id>,<next_simulation_time_in_hours>),

#party_set_name                         = 1669   # (party_set_name, <party_id>, <string_no>),

#party_add_xp_to_stack                  = 1670   # (party_add_xp_to_stack, <party_id>, <stack_no>, <xp_amount>),

party_get_morale > party_morale[X],0 # 0 = party_id

#party_set_morale                       = 1672   # (party_set_morale, <party_id>, <value>), #value is clamped to range [0...100].

#party_upgrade_with_xp                  = 1673   # (party_upgrade_with_xp, <party_id>, <xp_amount>, <upgrade_path>), #upgrade_path can be:
#                                                                                                                    #0 = choose random, 1 = choose first, 2 = choose second
#party_add_xp                           = 1674   # (party_add_xp, <party_id>, <xp_amount>),

#party_add_template                     = 1675   # (party_add_template, <party_id>, <party_template_id>, [reverse_prisoner_status]),

#party_set_icon                         = 1676   # (party_set_icon, <party_id>, <map_icon_id>),
#party_set_banner_icon                  = 1677   # (party_set_banner_icon, <party_id>, <map_icon_id>),
#party_add_particle_system              = 1678   # (party_add_particle_system, <party_id>, <particle_system_id>),
#party_clear_particle_systems           = 1679   # (party_clear_particle_systems, <party_id>),

party_get_battle_opponent > party_battle_opponent[X],0 # 0 = party_id

party_get_icon > party_icon[X],0 # 0 = party_id

#party_set_extra_icon                   = 1682   # (party_set_extra_icon, <party_id>, <map_icon_id>, <up_down_distance_fixed_point>, <up_down_frequency_fixed_point>, <rotate_frequency_fixed_point>, <fade_in_out_frequency_fixed_point>), #frequencies are in number of revolutions per second

party_get_skill_level > party_skill_lvl[X],0,0 # 0 = party_id, 0 = skill_no

agent_get_speed > agent_speed[X],0 # 0 = agent_id

get_battle_advantage > battle_advantage

#set_battle_advantage                   = 1691   # (set_battle_advantage, <value>),

#agent_refill_wielded_shield_hit_points = 1692   # (agent_refill_wielded_shield_hit_points, <agent_id>),
#agent_is_in_special_mode               = 1693   # (agent_is_in_special_mode,<agent_id>),

party_get_attached_to > party_attached[X],0 # 0 = party_id

party_get_num_attached_parties > party_num_attached_parties[X],0 # 0 = party_id

party_get_attached_party_with_rank > party_attached_party_with_rank[X],0,0 # 0 = party_id, 0 = attached_party_id

#inflict_casualties_to_party_group      = 1697   # (inflict_casualties_to_party, <parent_party_id>, <attack_rounds>, <party_id_to_add_causalties_to>), 
#distribute_party_among_party_group     = 1698   # (distribute_party_among_party_group, <party_to_be_distributed>, <group_root_party>),
#agent_is_routed                        = 1699   # (agent_is_routed,<agent_id>),

#Agents

#store_distance_between_positions,
#position_is_behind_poisiton,
get_player_agent_no > agent_no[X]

get_player_agent_kill_count > agent_kill_count[X],[0] # 0 = get_wounded (returns wounded if non zero)

#agent_is_alive                         = 1702	# (agent_is_alive,<agent_id>),
#agent_is_wounded                       = 1703	# (agent_is_wounded,<agent_id>),
#agent_is_human                         = 1704	# (agent_is_human,<agent_id>),

get_player_agent_own_troop_kill_count > agent_own_troop_kill_count[X],[0] # 0 = get_wounded (returns wounded if non zero)

#agent_is_ally                          = 1706	# (agent_is_ally,<agent_id>),
#agent_is_non_player                    = 1707   # (agent_is_non_player, <agent_id>),
#agent_is_defender                      = 1708	# (agent_is_defender,<agent_id>),

#agent_get_look_position                = 1709   # (agent_get_look_position, <position_no>, <agent_id>),

#agent_get_position                     = 1710	# (agent_get_position,<position_no>,<agent_id>),

#agent_set_position                     = 1711	# (agent_set_position,<agent_id>,<position_no>),
#agent_is_active                        = 1712   # (agent_is_active,<agent_id>),
#agent_set_look_target_agent            = 1713 # (agent_set_look_target_agent, <agent_id>, <agent_id>), #second agent_id is the target

agent_get_horse > agent_horse[X],0 # 0 = agent_id

agent_get_rider > agent_rider[X],0 # 0 = agent_id

agent_get_party_id > party_id[X],0 # 0 = agent_id

agent_get_entry_no > agent_entry_no[X],0 # 0 = agent_id

agent_get_troop_id > troop_id[X],0 # 0 = agent_id

agent_get_item_id > agent_item_id[X],0 # 0 = agent_id (works only for horses ???)

store_agent_hit_points > agent_hit_points[X],0,[0] # V explained V
#						# (store_agent_hit_points,<destination>,<agent_id>,[absolute]),
#agent_set_hit_points                   = 1721	# set absolute to 1 if value is absolute, otherwise value will be treated as relative number in range [0..100]
#						# (agent_set_hit_points,<agent_id>,<value>,[absolute]),

#agent_deliver_damage_to_agent          = 1722	# (agent_deliver_damage_to_agent, <agent_id_deliverer>, <agent_id>, <value>, [item_id]), #if value <= 0, then damage will be calculated using the weapon item. # item_id is the item that the damage is delivered. can be ignored.

agent_get_kill_count > agent_kill_count[X],0,[0] # 0 = agent_id, 0 = get_wounded #Set second value to non-zero to get wounded count

agent_get_player_id > agent_player_id[X],0 # 0 = agent_id

#agent_set_invulnerable_shield          = 1725 # (agent_set_invulnerable_shield, <agent_id>),

agent_get_wielded_item > agent_wieled_item[X],0,0 # 0 = agent_id, 0 = hand_no

agent_get_ammo > agent_ammo[X],0,0 # 0 = agent_id, 0 = value; #value = 1 gets ammo for wielded item, value = 0 gets ammo for all items

#agent_refill_ammo                      = 1728	# (agent_refill_ammo,<agent_id>),
#agent_has_item_equipped                = 1729	# (agent_has_item_equipped,<agent_id>,<item_id>),

#agent_set_scripted_destination         = 1730	# (agent_set_scripted_destination,<agent_id>,<position_no>,<auto_set_z_to_ground_level>,<no_rethink>), #auto_set_z_to_ground_level can be 0 (false) or 1 (true), no_rethink = 1 to save resources

#agent_get_scripted_destination         = 1731   # (agent_get_scripted_destination,<position_no>,<agent_id>),

#agent_force_rethink                    = 1732 # (agent_force_rethink, <agent_id>),
#agent_set_no_death_knock_down_only     = 1733 # (agent_set_no_death_knock_down_only, <agent_id>, <value>), #0 for disable, 1 for enable
#agent_set_horse_speed_factor           = 1734 # (agent_set_horse_speed_factor, <agent_id>, <speed_multiplier-in-1/100>),
#agent_clear_scripted_mode              = 1735	# (agent_clear_scripted_mode,<agent_id>),
#agent_set_speed_limit                  = 1736   # (agent_set_speed_limit,<agent_id>,<speed_limit(kilometers/hour)>), #Affects AI only 
#agent_ai_set_always_attack_in_melee    = 1737   # (agent_ai_set_always_attack_in_melee, <agent_id>,<value>), #to be used in sieges so that agents don't wait on the ladder.

agent_get_simple_behavior > agent_simple_behavior[X],0 # 0 = agent_id #constants are written in header_mission_templates.py, starting with aisb_

agent_get_combat_state > agent_combat_state[X],0 # 0 = agent_id

#agent_set_animation                    = 1740   # (agent_set_animation, <agent_id>, <anim_id>, [channel_no]), #channel_no default is 0. Top body only animations should have channel_no value as 1.
#agent_set_stand_animation              = 1741   # (agent_set_stand_action, <agent_id>, <anim_id>),
#agent_set_walk_forward_animation       = 1742   # (agent_set_walk_forward_action, <agent_id>, <anim_id>),
#agent_set_animation_progress           = 1743   # (agent_set_animation_progress, <agent_id>, <value_fixed_point>), #value should be between 0-1 (as fixed point)
#agent_set_look_target_position         = 1744   # (agent_set_look_target_position, <agent_id>, <position_no>),
#agent_set_attack_action                = 1745   # (agent_set_attack_action, <agent_id>, <value>, <value>), #value: -2 = clear any attack action, 0 = thrust, 1 = slashright, 2 = slashleft, 3 = overswing - second value 0 = ready and release, 1 = ready and hold
#agent_set_defend_action                = 1746   # (agent_set_defend_action, <agent_id>, <value>, <duration-in-1/1000-seconds>), #value_1: -2 = clear any defend action, 0 = defend_down, 1 = defend_right, 2 = defend_left, 3 = defend_up
#agent_set_wielded_item                 = 1747   # (agent_set_wielded_item, <agent_id>, <item_id>),
#agent_set_scripted_destination_no_attack = 1748	# (agent_set_scripted_destination_no_attack,<agent_id>,<position_no>,<auto_set_z_to_ground_level>), #auto_set_z_to_ground_level can be 0 (false) or 1 (true)
#agent_fade_out                         = 1749   # (agent_fade_out, <agent_id>),
#agent_play_sound                       = 1750   # (agent_play_sound, <agent_id>, <sound_id>),
#agent_start_running_away               = 1751   # (agent_start_running_away, <agent_id>, [position_no]), # if position no is entered, agent will run away to that location. pos0 is not allowed (will be ignored).
#agent_stop_running_away                = 1752   # (agent_stop_run_away, <agent_id>),
#agent_ai_set_aggressiveness            = 1753   # (agent_ai_set_aggressiveness, <agent_id>, <value>), #100 is the default aggressiveness. higher the value, less likely to run back
#agent_set_kick_allowed                 = 1754   # (agent_set_kick_allowed, <agent_id>, <value>), #0 for disable, 1 for allow

#remove_agent                           = 1755   # (remove_agent, <agent_id>),

agent_get_attached_scene_prop > agent_attached_scene_prop[X],0 # 0 = agent_id

#agent_set_attached_scene_prop          = 1757   # (agent_set_attached_scene_prop, <agent_id>, <scene_prop_id>)
#agent_set_attached_scene_prop_x        = 1758   # (agent_set_attached_scene_prop_x, <agent_id>, <value>)
#agent_set_attached_scene_prop_z        = 1759   # (agent_set_attached_scene_prop_z, <agent_id>, <value>)

agent_get_time_elapsed_since_removed > agent_time_since_removed[X],0 # 0 = agent_id

agent_get_number_of_enemies_following > agent_num_following_enemies[X],0 # 0 = agent_id

#agent_set_no_dynamics                  = 1762   # (agent_set_no_dynamics, <agent_id>, <value>), #0 = turn dynamics off, 1 = turn dynamics on (required for cut-scenes)

agent_get_attack_action > agent_attack_action[X],0 # 0 = agent_id #returned values: free = 0, readying_attack = 1, releasing_attack = 2, completing_attack_after_hit = 3, attack_parried = 4, reloading = 5, after_release = 6, cancelling_attack = 7

agent_get_defend_action > agent_defend_action[X],0 # 0 = agent_id #returned values: free = 0, parrying = 1, blocking = 2

agent_get_group > agent_group[X],0 # 0 = agent_id

#agent_set_group                        = 1766   # (agent_set_group, <agent_id>, <value>),

agent_get_action_dir > agent_action_direction[X],0 # (agent_get_action_dir, <destination>, <agent_id>), #invalid = -1, down = 0, right = 1, left = 2, up = 3

agent_get_animation > agent_anim[X],0,0 # (agent_get_animation, <destination>, <agent_id>, <body_part), #0 = lower body part, 1 = upper body part

#agent_is_in_parried_animation          = 1769   # (agent_is_in_parried_animation, <agent_id>),

agent_get_team > agent_team_no[X],0 # 0 = agent_id

#agent_set_team                         = 1771   # (agent_set_team  , <agent_id>, <value>),

agent_get_class > agent_class[X],0 # 0 = agent_id

agent_get_division > agent_division[X],0 # 0 = agent_id

#agent_unequip_item                     = 1774	  # (agent_unequip_item, <agent_id>, <item_id>, [weapon_slot_no]), #weapon_slot_no is optional, and can be between 1-4 (used only for weapons, not armor). in either case, item_id has to be set correctly.

#class_is_listening_order               = 1775   # (class_is_listening_order, <team_no>, <sub_class>),
#agent_set_ammo                         = 1776   # (agent_set_ammo,<agent_id>,<item_id>,<value>), #value = a number between 0 and maximum ammo

#agent_add_offer_with_timeout           = 1777   # (agent_add_offer_with_timeout, <agent_id>, <agent_id>, <duration-in-1/1000-seconds>), #second agent_id is offerer, 0 value for duration is an infinite offer
#agent_check_offer_from_agent           = 1778   # (agent_check_offer_from_agent, <agent_id>, <agent_id>), #second agent_id is offerer

#agent_equip_item                       = 1779	  # (agent_equip_item, <agent_id>, <item_id>, [weapon_slot_no]), #for weapons, agent needs to have an empty weapon slot. weapon_slot_no is optional, and can be between 1-4 (used only for weapons, not armor).

#entry_point_get_position               = 1780   # (entry_point_get_position, <position_no>, <entry_no>),

#entry_point_set_position               = 1781   # (entry_point_set_position, <entry_no>, <position_no>),
#entry_point_is_auto_generated          = 1782  	# (entry_point_is_auto_generated, <entry_no>),

#agent_set_division                     = 1783   # (agent_set_division, <agent_id>, <value>),

team_get_hold_fire_order > team_hold_fire_order[X],0,0 # 0 = team_no, 0 = sub_class

team_get_movement_order > team_movement_order[X],0,0 # 0 = team_no, 0 = sub_class

team_get_riding_order > team_riding_order[X],0,0 # 0 = team_no, 0 = sub_class

team_get_weapon_usage_order > team_weapon_usage_order[X],0,0 # 0 = team_no, 0 = sub_class

#teams_are_enemies                      = 1788   # (teams_are_enemies, <team_no>, <team_no_2>), 
#team_give_order                        = 1790   # (team_give_order, <team_no>, <sub_class>, <order_id>),
#team_set_order_position                = 1791   # (team_set_order_position, <team_no>, <sub_class>, <position_no>),

team_get_leader > team_leader[X],0 # 0 = team_no

#team_set_leader                        = 1793   # (team_set_leader, <team_no>, <new_leader_agent_id>),

#team_get_order_position                = 1794   # (team_get_order_position, <position_no>, <team_no>, <sub_class>),

#team_set_order_listener                = 1795   # (team_set_order_listener, <team_no>, <sub_class>, <value>), #merge with old listeners if value is non-zero #clear listeners if sub_class is less than zero
#team_set_relation                      = 1796   # (team_set_relation, <team_no>, <team_no_2>, <value>), # -1 for enemy, 1 for friend, 0 for neutral

#set_rain                               = 1797   # (set_rain,<rain-type>,<strength>), (rain_type: 1= rain, 2=snow ; strength: 0 - 100)
#set_fog_distance                       = 1798   # (set_fog_distance, <distance_in_meters>, [fog_color]),

#get_scene_boundaries                   = 1799   # (get_scene_boundaries, <position_min>, <position_max>),

#scene_prop_enable_after_time           = 1800   # (scene_prop_enable_after_time, <scene_prop_id>, <value>)
#scene_prop_has_agent_on_it             = 1801   # (scene_prop_has_agent_on_it, <scene_prop_id>, <agent_id>)

#agent_clear_relations_with_agents      = 1802   # (agent_clear_relations_with_agents, <agent_id>),
#agent_add_relation_with_agent          = 1803   # (agent_add_relation_with_agent, <agent_id>, <agent_id>, <value>), #-1 = enemy, 0 = neutral (no friendly fire at all), 1 = ally

agent_get_item_slot > agent_item_slot[X],0,0 # 0 = agent_id, 0 = value # between 0-7, order is weapon1, weapon2, weapon3, weapon4, head_armor, body_armor, leg_armor, hand_armor

#ai_mesh_face_group_show_hide           = 1805   # (ai_mesh_face_group_show_hide, <group_no>, <value>), # 1 for enable, 0 for disable

#agent_is_alarmed                       = 1806   # (agent_is_alarmed, <agent_id>),
#agent_set_is_alarmed                   = 1807   # (agent_set_is_alarmed, <agent_id>, <value>), # 1 for enable, 0 for disable
#agent_stop_sound                       = 1808   # (agent_stop_sound, <agent_id>),
#agent_set_attached_scene_prop_y        = 1809   # (agent_set_attached_scene_prop_y, <agent_id>, <value>)

scene_prop_get_num_instances > scp_num_instances[X],0 # 0 = scene_prop_id

scene_prop_get_instance > scp_instance[X],0,0 # 0 = scene_prop_id, 0 = instance_no

scene_prop_get_visibility > scp_visibility[X],0 # 0 = scene_prop_id

#scene_prop_set_visibility              = 1813   # (scene_prop_set_visibility, <scene_prop_id>, <value>),
#scene_prop_set_hit_points              = 1814   # (scene_prop_set_hit_points, <scene_prop_id>, <value>),

scene_prop_get_hit_points > scp_hit_points[X],0 # 0 = scene_prop_id

scene_prop_get_max_hit_points > scp_max_hit_points[X],0 # 0 = scene_prop_id

scene_prop_get_team > team_no[X],0 # 0 = scene_prop_id

#scene_prop_set_team                    = 1818   # (scene_prop_set_team, <scene_prop_id>, <value>),
#scene_prop_set_prune_time              = 1819   # (scene_prop_set_prune_time, <scene_prop_id>, <value>), # prune time can only be set to objects that are already on the prune queue. static objects are not affected by this operation.
#scene_prop_set_cur_hit_points          = 1820   # (scene_prop_set_cur_hit_points, <scene_prop_id>, <value>),

#scene_prop_fade_out                    = 1822   # (scene_prop_fade_out, <scene_prop_id>, <fade_out_time>)
#scene_prop_fade_in                     = 1823   # (scene_prop_fade_in, <scene_prop_id>, <fade_in_time>)

agent_get_ammo_for_slot > agent_ammo_for_slot[X],0,0 # 0 = agent_id, 0 = slot_no

#agent_is_in_line_of_sight              = 1826 # (agent_is_in_line_of_sight, <agent_id>, <position_no>), # rotation of the position register is not used.
#agent_deliver_damage_to_agent_advanced = 1827	# (agent_deliver_damage_to_agent_advanced, <destination>, <agent_id_deliverer>, <agent_id>, <value>, [item_id]), #if value <= 0, then damage will be calculated using the weapon item. # item_id is the item that the damage is delivered. can be ignored.
#this advanced mode of agent_deliver_damage_to_agent has 2 differences. 1- the delivered damage is returned. 2- the damage delivery is done after checking the relationship between agents. this might cause no damage, or even damage to the shooter agent because of a friendly fire.

team_get_gap_distance > team_gap_distance[X],0,0 # 0 = team_no, 0 = sub_class

#add_missile                            = 1829	# (add_missile, <agent_id>, <starting_position>, <starting_speed_fixed_point>, <weapon_item_id>, <weapon_item_modifier>, <missile_item_id>, <missile_item_modifier>), # starting position also contains the direction of the arrow

scene_item_get_num_instances > item_num_instances[X],0 # 0 = item_id

scene_item_get_instance > item_instance[X],0,0 # 0 = item_id, 0 = instance_no

scene_spawned_item_get_num_instances > item_num_instances[X],0 # 0 = item_id

scene_spawned_item_get_instance > item_instance[X],0,0 # 0 = item_id, 0 = instance_no

#class_set_name                         = 1837 # (class_set_name, <sub_class>, <string_id>),

#prop_instance_is_valid                 = 1838 # (prop_instance_is_valid, <scene_prop_id>),

prop_instance_get_variation_id > prop_variation_id_[X],0 # 0 = scene_prop_id

prop_instance_get_variation_id_2 > prop_variation_id_2_[X],0 # 0 = scene_prop_id

#prop_instance_get_position             = 1850	# (prop_instance_get_position, <position_no>, <scene_prop_id>),

#prop_instance_get_starting_position    = 1851	# (prop_instance_get_starting_position, <position_no>, <scene_prop_id>),

#prop_instance_get_scale                = 1852	# (prop_instance_get_scale, <position_no>, <scene_prop_id>),

prop_instance_get_scene_prop_kind > scene_prop_kind[X],0 # 0 = scene_prop_id

#prop_instance_set_scale                = 1854 # (prop_instance_set_scale, <scene_prop_id>, <value_x_fixed_point>, <value_y_fixed_point>, <value_z_fixed_point>),
#prop_instance_set_position             = 1855	# (prop_instance_set_position, <scene_prop_id>, <position_no>, [dont_send_to_clients]),
#dont_send_to_clients default is 0, therefore it is sent to clients. if you are just doing some physics checks with scene props, then don't send them to clients
#prop_instance_animate_to_position      = 1860	# (prop_instance_animate_to_position, <scene_prop_id>, position, <duration-in-1/100-seconds>),
#prop_instance_stop_animating           = 1861	# (prop_instance_stop_animating, <scene_prop_id>),
#prop_instance_is_animating             = 1862   # (prop_instance_is_animating, <destination>, <scene_prop_id>),
#prop_instance_get_animation_target_position = 1863    # (prop_instance_get_animation_target_position, <pos>, <scene_prop_id>)
#prop_instance_enable_physics           = 1864   # (prop_instance_enable_physics, <scene_prop_id>, <value>), #0 for disable, 1 for enable
#prop_instance_rotate_to_position       = 1865	# (prop_instance_rotate_to_position, <scene_prop_id>, position, <duration-in-1/100-seconds>, <total_rotate_angle>),
#prop_instance_initialize_rotation_angles = 1866   # (prop_instance_initialize_rotation_angles, <scene_prop_id>),
#prop_instance_refill_hit_points        = 1870 # (prop_instance_refill_hit_points, <scene_prop_id>), 

#prop_instance_dynamics_set_properties  = 1871 # (prop_instance_dynamics_set_properties,<scene_prop_id>,mass_friction),
#prop_instance_dynamics_set_velocity    = 1872 # (prop_instance_dynamics_set_velocity,<scene_prop_id>,linear_velocity),
#prop_instance_dynamics_set_omega       = 1873 # (prop_instance_dynamics_set_omega,<scene_prop_id>,angular_velocity),
#prop_instance_dynamics_apply_impulse   = 1874 # (prop_instance_dynamics_apply_impulse,<scene_prop_id>,impulse_force),

#prop_instance_receive_damage           = 1877 # (prop_instance_receive_damage, <scene_prop_id>, <agent_id>, <damage_value>),

#prop_instance_intersects_with_prop_instance = 1880 # (prop_instance_intersects_with_prop_instance, <scene_prop_id>, <scene_prop_id>), #give second scene_prop_id as -1 to check all scene props.
#cannot check polygon-to-polygon physics models, but can check any other combinations between sphere, capsule and polygon physics models.

#prop_instance_play_sound               = 1881 # (prop_instance_play_sound, <scene_prop_id>, <sound_id>, [flags]), # sound flags can be given
#prop_instance_stop_sound               = 1882 # (prop_instance_stop_sound, <scene_prop_id>),

#prop_instance_clear_attached_missiles  = 1885 # (prop_instance_clear_attached_missiles, <scene_prop_id>), # Works only with dynamic scene props (non-retrievable missiles)

#prop_instance_add_particle_system      = 1886 # (prop_instance_add_particle_system, <scene_prop_id>, <par_sys_id>, <position_no>), # position is local, not global.
#prop_instance_stop_all_particle_systems= 1887 # (prop_instance_stop_all_particle_systems, <scene_prop_id>),


#replace_prop_instance                  = 1889   # (replace_prop_instance, <scene_prop_id>, <new_scene_prop_id>),
#replace_scene_props                    = 1890   # (replace_scene_props, <old_scene_prop_id>,<new_scene_prop_id>),
#replace_scene_items_with_scene_props   = 1891   # (replace_scene_items_with_scene_props, <old_item_id>,<new_scene_prop_id>),

cast_ray > casted_ray[X],0,0,[0] # = 1900   # (cast_ray, <destination>, <hit_position_register>, <ray_position_register>, [<ray_length_fixed_point>]), #Casts a ray of length [<ray_length_fixed_point>] starting from <ray_position_register> and stores the closest hit position into <hit_position_register> (fails if no hits). If the body hit is a prop instance, its id will be stored into <destination>

#---------------------------
# Mission Consequence types
#---------------------------

#set_mission_result                     = 1906	# (set_mission_result,<value>),
#finish_mission                         = 1907	# (finish_mission, <delay_in_seconds>),
#jump_to_scene                          = 1910	# (jump_to_scene,<scene_id>,<entry_no>),
#set_jump_mission                       = 1911	# (set_jump_mission,<mission_template_id>),
#set_jump_entry                         = 1912	# (set_jump_entry,<entry_no>),
#start_mission_conversation             = 1920	# (start_mission_conversation,<troop_id>),
#add_reinforcements_to_entry            = 1930	# (add_reinforcements_to_entry,<mission_template_entry_no>,<value>),

#mission_enable_talk                    = 1935   # (mission_enable_talk), #can talk with troops during battles
#mission_disable_talk                   = 1936   # (mission_disable_talk), #disables talk option for the mission

#mission_tpl_entry_set_override_flags   = 1940   # (mission_entry_set_override_flags, <mission_template_id>, <entry_no>, <value>),
#mission_tpl_entry_clear_override_items = 1941   # (mission_entry_clear_override_items, <mission_template_id>, <entry_no>),
#mission_tpl_entry_add_override_item    = 1942   # (mission_entry_add_override_item, <mission_template_id>, <entry_no>, <item_kind_id>),

#set_current_color                      = 1950	# red, green, blue: a value of 255 means 100%
#					          # (set_current_color,<value>,<value>,<value>),
#set_position_delta                     = 1955	# x, y, z
#                                                  # (set_position_delta,<value>,<value>,<value>),
#add_point_light                        = 1960	# (add_point_light,[flicker_magnitude],[flicker_interval]), #flicker_magnitude between 0 and 100, flicker_interval is in 1/100 seconds
#add_point_light_to_entity              = 1961	# (add_point_light_to_entity,[flicker_magnitude],[flicker_interval]), #flicker_magnitude between 0 and 100, flicker_interval is in 1/100 seconds
#particle_system_add_new                = 1965	# (particle_system_add_new,<par_sys_id>,[position_no]),
#particle_system_emit                   = 1968	# (particle_system_emit,<par_sys_id>,<value_num_particles>,<value_period>),
#particle_system_burst                  = 1969	# (particle_system_burst,<par_sys_id>,<position_no>,[percentage_burst_strength]),

#set_spawn_position                     = 1970   # (set_spawn_position, <position_no>), 
#spawn_item                             = 1971   # (spawn_item, <item_kind_id>, <item_modifier>, [seconds_before_pruning]), #if seconds_before_pruning = 0 then item never gets pruned
#spawn_agent                            = 1972	# (spawn_agent,<troop_id>), (stores agent_id in reg0)
#spawn_horse                            = 1973	# (spawn_horse,<item_kind_id>, <item_modifier>),  (stores agent_id in reg0)
#spawn_scene_prop                       = 1974   # (spawn_scene_prop, <scene_prop_id>),  (stores prop_instance_id in reg0) not yet.

#particle_system_burst_no_sync		   = 1975	# (particle_system_burst_without_sync,<par_sys_id>,<position_no>,[percentage_burst_strength]),

#spawn_item_without_refill              = 1976   # (spawn_item_without_refill, <item_kind_id>, <item_modifier>, [seconds_before_pruning]), #if seconds_before_pruning = 0 then item never gets pruned

agent_get_item_cur_ammo > agent_item_cur_ammo[X],0,0 # 0 = agent_id, 0 = slot_no

#cur_item_add_mesh                      = 1964   # (cur_item_add_mesh, <mesh_name_string_no>, [<lod_begin>], [<lod_end>]), #only call inside ti_on_init_item in module_items # lod values are optional. lod_end is not included.
#cur_item_set_material                  = 1978   # (cur_item_set_material, <string_no>, <sub_mesh_no>, [<lod_begin>], [<lod_end>]), #only call inside ti_on_init_item in module_items # lod values are optional. lod_end is not included.

#cur_tableau_add_tableau_mesh           = 1980   # (cur_tableau_add_tableau_mesh, <tableau_material_id>, <value>, <position_register_no>), #value is passed to tableau_material
#cur_item_set_tableau_material          = 1981   # (cur_item_set_tableu_material, <tableau_material_id>, <instance_code>), #only call inside ti_on_init_item in module_items; instance_code is simply passed as a parameter to the tableau
#cur_scene_prop_set_tableau_material    = 1982   # (cur_scene_prop_set_tableau_material, <tableau_material_id>, <instance_code>), #only call inside ti_on_init_scene_prop in module_scene_props; instance_code is simply passed as a parameter to the tableau
#cur_map_icon_set_tableau_material      = 1983   # (cur_map_icon_set_tableau_material, <tableau_material_id>, <instance_code>), #only call inside ti_on_init_map_icon in module_scene_props; instance_code is simply passed as a parameter to the tableau
#cur_tableau_render_as_alpha_mask       = 1984   # (cur_tableau_render_as_alpha_mask)
#cur_tableau_set_background_color       = 1985   # (cur_tableau_set_background_color, <value>),
#cur_agent_set_banner_tableau_material  = 1986   # (cur_agent_set_banner_tableau_material, <tableau_material_id>)
#cur_tableau_set_ambient_light          = 1987   # (cur_tableau_set_ambient_light, <red_fixed_point>, <green_fixed_point>, <blue_fixed_point>),
#cur_tableau_set_camera_position        = 1988   # (cur_tableau_set_camera_position, <position_no>),
#cur_tableau_set_camera_parameters      = 1989   # (cur_tableau_set_camera_parameters, <is_perspective>, <camera_width_times_1000>, <camera_height_times_1000>, <camera_near_times_1000>, <camera_far_times_1000>),
#cur_tableau_add_point_light            = 1990   # (cur_tableau_add_point_light, <map_icon_id>, <position_no>, <red_fixed_point>, <green_fixed_point>, <blue_fixed_point>),
#cur_tableau_add_sun_light              = 1991   # (cur_tableau_add_sun_light, <map_icon_id>, <position_no>, <red_fixed_point>, <green_fixed_point>, <blue_fixed_point>),
#cur_tableau_add_mesh                   = 1992   # (cur_tableau_add_mesh, <mesh_id>, <position_no>, <value_fixed_point>, <value_fixed_point>),
#                                                # first value fixed point is the scale factor, second value fixed point is alpha. use 0 for default values
#cur_tableau_add_mesh_with_vertex_color = 1993   # (cur_tableau_add_mesh_with_vertex_color, <mesh_id>, <position_no>, <value_fixed_point>, <value_fixed_point>, <value>),
#                                                # first value fixed point is the scale factor, second value fixed point is alpha. value is vertex color. use 0 for default values. vertex_color has no default value.

#cur_tableau_add_map_icon               = 1994   # (cur_tableau_add_map_icon, <map_icon_id>, <position_no>, <value_fixed_point>),
#                                                # value fixed point is the scale factor
                                                
#cur_tableau_add_troop                  = 1995   # (cur_tableau_add_troop, <troop_id>, <position_no>, <animation_id>, <instance_no>), #if instance_no value is 0 or less, then the face is not generated randomly (important for heroes)
#cur_tableau_add_horse                  = 1996   # (cur_tableau_add_horse, <item_id>, <position_no>, <animation_id>),
#cur_tableau_set_override_flags         = 1997   # (cur_tableau_set_override_flags, <value>),
#cur_tableau_clear_override_items       = 1998   # (cur_tableau_clear_override_items),
#cur_tableau_add_override_item          = 1999   # (cur_tableau_add_override_item, <item_kind_id>),
#cur_tableau_add_mesh_with_scale_and_vertex_color = 2000   # (cur_tableau_add_mesh_with_scale_and_vertex_color, <mesh_id>, <position_no>, <position_no>, <value_fixed_point>, <value>),
#                                                # second position_no is x,y,z scale factors (with fixed point values). value fixed point is alpha. value is vertex color. use 0 for default values. scale and vertex_color has no default values.
 
#mission_cam_set_mode                   = 2001   # (mission_cam_set_mode, <mission_cam_mode>, <duration-in-1/1000-seconds>, <value>), # when leaving manual mode, duration defines the animation time from the initial position to the new position. set as 0 for instant camera position update
#                                                                                                                                    # if value = 0, then camera velocity will be linear. else it will be non-linear

mission_get_time_speed > mission_time_speed

#mission_set_time_speed                 = 2003   # (mission_set_time_speed, <value_fixed_point>), #this works only when cheat mode is enabled
#mission_time_speed_move_to_value       = 2004   # (mission_speed_move_to_value, <value_fixed_point>, <duration-in-1/1000-seconds>), #this works only when cheat mode is enabled
#mission_set_duel_mode                  = 2006   # (mission_set_duel_mode, <value>), #value: 0 = off, 1 = on

#mission_cam_set_screen_color           = 2008   #(mission_cam_set_screen_color, <value>), #value is color together with alpha
#mission_cam_animate_to_screen_color    = 2009   #(mission_cam_animate_to_screen_color, <value>, <duration-in-1/1000-seconds>), #value is color together with alpha

#mission_cam_get_position               = 2010   # (mission_cam_get_position, <position_register_no>),

#mission_cam_set_position               = 2011   # (mission_cam_set_position, <position_register_no>),
#mission_cam_animate_to_position        = 2012   # (mission_cam_animate_to_position, <position_register_no>, <duration-in-1/1000-seconds>, <value>), # if value = 0, then camera velocity will be linear. else it will be non-linear

mission_cam_get_aperture > mission_cam_aperture

#mission_cam_set_aperture               = 2014   # (mission_cam_set_aperture, <value>),
#mission_cam_animate_to_aperture        = 2015   # (mission_cam_animate_to_aperture, <value>, <duration-in-1/1000-seconds>, <value>), # if value = 0, then camera velocity will be linear. else it will be non-linear
#mission_cam_animate_to_position_and_aperture = 2016   # (mission_cam_animate_to_position_and_aperture, <position_register_no>, <value>, <duration-in-1/1000-seconds>, <value>), # if value = 0, then camera velocity will be linear. else it will be non-linear
#mission_cam_set_target_agent           = 2017   # (mission_cam_set_target_agent, <agent_id>, <value>), #if value = 0 then do not use agent's rotation, else use agent's rotation
#mission_cam_clear_target_agent         = 2018   # (mission_cam_clear_target_agent),
#mission_cam_set_animation              = 2019   # (mission_cam_set_animation, <anim_id>),

#talk_info_show                         = 2020   # (talk_info_show, <hide_or_show>), :0=hide 1=show
#talk_info_set_relation_bar             = 2021   # (talk_info_set_relation_bar, <value>), :set relation bar to a value between -100 to 100, enter an invalid value to hide the bar.
#talk_info_set_line                     = 2022   # (talk_info_set_line, <line_no>, <string_no>)

#mesh related
#set_background_mesh                    = 2031   # (set_background_mesh, <mesh_id>),
#set_game_menu_tableau_mesh             = 2032   # (set_game_menu_tableau_mesh, <tableau_material_id>, <value>, <position_register_no>), #value is passed to tableau_material
#                                                # position contains the following information: x = x position of the mesh, y = y position of the mesh, z = scale of the mesh

#change_window types.
#change_screen_loot                     = 2041	# (change_screen_loot, <troop_id>),
#change_screen_trade                    = 2042	# (change_screen_trade, <troop_id>),
#change_screen_exchange_members         = 2043	# (change_screen_exchange_members, [0,1 = exchange_leader], [party_id]), #if party id is not given, current party will be used
#change_screen_map_conversation         = 2049   # (change_screen_map_conversation, <troop_id>),
#                                                # Starts the mission, same as (change_screen_mission). However once the mission starts, player will get into dialog with the specified troop, and once the dialog ends, the mission will automatically end.
#change_screen_exchange_with_party      = 2050   # (change_screen_exchange_with_party, <party_id>),
#change_screen_equip_other              = 2051	# (change_screen_equip_other, <troop_id>),
#change_screen_notes                    = 2053   # (change_screen_notes, <note_type>, <object_id>), #Note type can be 1 = troops, 2 = factions, 3 = parties, 4 = quests, 5 = info_pages
#change_screen_give_members             = 2056   # (change_screen_give_members, [party_id]), #if party id is not given, current party will be used

#jump_to_menu                           = 2060	# (jump_to_menu,<menu_id>),

store_trigger_param > trigger_param_[X],0 # 0 = param_no
store_trigger_param_1 > trigger_param_1_[X]
store_trigger_param_2 > trigger_param_2_[X]
store_trigger_param_3 > trigger_param_3_[X]
#set_trigger_result                     = 2075  # (set_trigger_result, <value>),

#agent_get_bone_position                = 2076 # (agent_get_bone_position, <position_no>, <agent_no>, <bone_no>, [<local_or_global>]), # returns position of bone. Option 0 for local to agent position, 1 for global

#agent_ai_set_interact_with_player      = 2077 # (agent_ai_set_interact_with_player, <agent_no>, <value>), # 0 for disable, 1 for enable.

agent_ai_get_look_target > agent_ai_look_target[X],0 # 0 = agent_id

agent_ai_get_move_target > agent_ai_move_target[X],0 # 0 = agent_id

agent_ai_get_behavior_target > agent_ai_behavior_target[X],0 # 0 = agent_id

#agent_ai_set_can_crouch                = 2083 # (agent_ai_set_can_crouch, <agent_id>, <value>), # 0 for false, 1 for true.

#agent_set_max_hit_points               = 2090	# set absolute to 1 if value is absolute, otherwise value will be treated as relative number in range [0..100]
#						# (agent_set_max_hit_points,<agent_id>,<value>,[absolute]),
#agent_set_damage_modifier              = 2091   # (agent_set_damage_modifier, <agent_id>, <value>), # value is in percentage, 100 is default
#agent_set_accuracy_modifier            = 2092   # (agent_set_accuracy_modifier, <agent_id>, <value>), # value is in percentage, 100 is default, value can be between [0..1000]
#agent_set_speed_modifier               = 2093   # (agent_set_speed_modifier, <agent_id>, <value>), # value is in percentage, 100 is default, value can be between [0..1000]
#agent_set_reload_speed_modifier        = 2094   # (agent_set_reload_speed_modifier, <agent_id>, <value>), # value is in percentage, 100 is default, value can be between [0..1000]
#agent_set_use_speed_modifier           = 2095   # (agent_set_use_speed_modifier, <agent_id>, <value>), # value is in percentage, 100 is default, value can be between [0..1000]
#agent_set_visibility                   = 2096   # (agent_set_visibility, <agent_id>, <value>), # 0 for invisible, 1 for visible.

agent_get_crouch_mode > agent_crouch_mode[X],0 # 0 = agent_id

#agent_set_crouch_mode                  = 2098   # (agent_ai_set_crouch_mode, <agent_id>, <value>), # 0-1
#agent_set_ranged_damage_modifier       = 2099   # (agent_set_ranged_damage_modifier, <agent_id>, <value>), # value is in percentage, 100 is default

#val_lshift             = 2100 # (val_lshift, <destination>, <value>), # shifts the bits of destination to left by value amount.
#val_rshift             = 2101 # (val_rshift, <destination>, <value>), # shifts the bits of destination to right by value amount.

#val_add                = 2105	#dest, operand ::       dest = dest + operand
#				# (val_add,<destination>,<value>),
#val_sub                = 2106	#dest, operand ::       dest = dest + operand
#				# (val_sub,<destination>,<value>),
#val_mul                = 2107	#dest, operand ::       dest = dest * operand
#				# (val_mul,<destination>,<value>),
#val_div                = 2108	#dest, operand ::       dest = dest / operand
#				# (val_div,<destination>,<value>),
#val_mod                = 2109	#dest, operand ::       dest = dest mod operand
#				# (val_mod,<destination>,<value>),
#val_min                = 2110	#dest, operand ::       dest = min(dest, operand)
#				# (val_min,<destination>,<value>),
#val_max                = 2111	#dest, operand ::       dest = max(dest, operand)
#				# (val_max,<destination>,<value>),
#val_clamp              = 2112	#dest, operand ::       dest = max(min(dest,<upper_bound> - 1),<lower_bound>)
#				# (val_clamp,<destination>,<lower_bound>, <upper_bound>),
#val_abs                = 2113  #dest          ::       dest = abs(dest)
#                                # (val_abs,<destination>),
#val_or                 = 2114   #dest, operand ::       dest = dest | operand
#				# (val_or,<destination>,<value>),
#val_and                = 2115   #dest, operand ::       dest = dest & operand
#				# (val_and,<destination>,<value>),
#store_or               = 2116   #dest, op1, op2 :      dest = op1 | op2
#                                # (store_or,<destination>,<value>,<value>),
#store_and              = 2117   #dest, op1, op2 :      dest = op1 & op2
#                                # (store_and,<destination>,<value>,<value>),

#store_mod              = 2119	#dest, op1, op2 :      dest = op1 % op2
#				# (store_mod,<destination>,<value>,<value>),
#store_add              = 2120	#dest, op1, op2 :      dest = op1 + op2
#				# (store_add,<destination>,<value>,<value>),
#store_sub              = 2121	#dest, op1, op2 :      dest = op1 - op2
#				# (store_sub,<destination>,<value>,<value>),
#store_mul              = 2122	#dest, op1, op2 :      dest = op1 * op2
#				# (store_mul,<destination>,<value>,<value>),
#store_div              = 2123	#dest, op1, op2 :      dest = op1 / op2
#				# (store_div,<destination>,<value>,<value>),

#set_fixed_point_multiplier      = 2124 # (set_fixed_point_multiplier, <value>),
#                                        # sets the precision of the values that are named as value_fixed_point or destination_fixed_point.
#                                        # Default is 1 (every fixed point value will be regarded as an integer)

#store_sqrt             = 2125  # (store_sqrt, <destination_fixed_point>, <value_fixed_point>), takes square root of the value
#store_pow              = 2126  # (store_pow, <destination_fixed_point>, <value_fixed_point>, <value_fixed_point), takes square root of the value
                                #dest, op1, op2 :      dest = op1 ^ op2
#store_sin              = 2127  # (store_sin, <destination_fixed_point>, <value_fixed_point>), takes sine of the value that is in degrees
#store_cos              = 2128  # (store_cos, <destination_fixed_point>, <value_fixed_point>), takes cosine of the value that is in degrees
#store_tan              = 2129  # (store_tan, <destination_fixed_point>, <value_fixed_point>), takes tangent of the value that is in degrees

#convert_to_fixed_point = 2130  # (convert_to_fixed_point, <destination_fixed_point>), multiplies the value with the fixed point multiplier
#convert_from_fixed_point= 2131 # (convert_from_fixed_point, <destination>), divides the value with the fixed point multiplier

#assign                 = 2133	# had to put this here so that it can be called from conditions.
#				# (assign,<destination>,<value>),

store_random > random_x[X]# deprecated, use store_random_in_range instead.

store_random_in_range > random_x[X],0,0 # gets random number in range [range_low,range_high] excluding range_high 
#				# (store_random_in_range,<destination>,<range_low>,<range_high>),

#store_asin             = 2140  # (store_asin, <destination_fixed_point>, <value_fixed_point>),
#store_acos             = 2141  # (store_acos, <destination_fixed_point>, <value_fixed_point>),
#store_atan             = 2142  # (store_atan, <destination_fixed_point>, <value_fixed_point>),
#store_atan2            = 2143  # (store_atan2, <destination_fixed_point>, <value_fixed_point>, <value_fixed_point>), #first value is y, second is x

store_troop_gold > troop_gold[X],0 # 0 = troop_id

store_num_free_stacks > party_num_free_stacks[X],0 # 0 = party_id

store_num_free_prisoner_stacks > party_num_free_prisoner_stacks[X],0 # 0 = party_id

store_party_size > party_size[X],[0] # 0 = party_id

store_party_size_wo_prisoners > party_size_wo_prisoners[X],[0] # 0 = party_id - without prisoners
#store_troop_kind_count          = 2158 # deprecated, use party_count_members_of_type instead
store_num_regular_prisoners > party_num_regular_prisoners[X],0 # 0 = party_id

store_troop_count_companions > troop_count_companions[X],0,[0] #0 = troop_id, 0 = party_id

store_troop_count_prisoners > troop_count_prisoners[X],0,[0] #0 = troop_id, 0 = party_id

store_item_kind_count > item_kind_count[X],0,[0] #0 = item_id, 0 = troop_id

store_free_inventory_capacity > free_inv_capacity[X],[0] #0 = troop_id

store_skill_level > skill_lvl[X],0,[0] # 0 = skill_level, 0 = troop_id

store_character_level > character_lvl[X],[0] #0 = troop_id

store_attribute_level > attribute_lvl[X],0,0 #0 = troop_id, 0 = attribute_id

store_troop_faction > troop_faction[X],0 #0 = troop_id
store_faction_of_troop > troop_faction[X],0 #0 = troop_id

store_troop_health > troop_health[X],0,[0] #0 = troop_id, 0 = absolute; # (store_troop_health,<destination>,<troop_id>,[absolute]),
#                                       # set absolute to 1 to get actual health; otherwise this will return percentage health in range (0-100)

store_proficiency_level > proficiency_lvl[X],0,0 #0 = troop_id, 0 = attribute_id

store_relation > faction_relation[X],0,0 #0 = faction_id(1), 0 = faction_id(2)
#set_conversation_speaker_troop  = 2197  # (set_conversation_speaker_troop, <troop_id>),
#                                        # Allows to dynamically switch speaking troops during the dialog when developer doesn't know in advance who will be doing the speaking. Should be placed in post-talk code section of dialog entry.
#set_conversation_speaker_agent  = 2198  # (set_conversation_speaker_troop, <agent_id>),
#                                        # Allows to dynamically switch speaking agents during the dialog when developer doesn't know in advance who will be doing the speaking. Should be placed in post-talk code section of dialog entry.
store_conversation_agent > talk_agent[X]
#                                        # Stores identifier of agent who is currently speaking.
store_conversation_troop > talk_troop[X]
#                                        # Stores identifier of troop who is currently speaking.
store_partner_faction > partner_faction[X]

store_encountered_party > encountered_party_[X]

store_encountered_party2 > encountered_party2_[X]

store_faction_of_party > party_faction[X],0 # 0 = party_id
#set_encountered_party           = 2205  # (set_encountered_party,<destination>),

store_current_scene > cur_scene[X]

store_zoom_amount > zoom_amount[X]

#set_zoom_amount                 = 2221 # (set_zoom_amount, <value_fixed_point>),
#is_zoom_disabled                = 2222 # (is_zoom_disabled),

store_item_value > item_value[X],0 # 0 = item_id

store_troop_value > troop_value[X],0 # 0 = troop_id

store_partner_quest > partner_quest[X]

store_random_quest_in_range > random_quest[X],0,0 # 0 = item_id -> <destination>,<lower_bound>,<upper_bound>

store_random_troop_to_raise > random_troop_to_raise[X],0,0 # (store_random_troop_to_raise,<destination>,<lower_bound>,<upper_bound>),

store_random_troop_to_capture > random_troop_to_capture[X],0,0 # (store_random_troop_to_capture,<destination>,<lower_bound>,<upper_bound>),

store_random_party_in_range > random_party[X],0,0 # (store_random_party_in_range,<destination>,<lower_bound>,<upper_bound>),

#store01_random_parties_in_range = 2255 # stores two random, different parties in a range to reg0 and reg1.
#					# (store01_random_parties_in_range,<lower_bound>,<upper_bound>),

store_random_horse > random_horse[X] # (store_random_horse,<destination>)

store_random_equipment > random_equipment[X] # (store_random_equipment,<destination>)

store_random_armor > random_armor[X] # (store_random_armor,<destination>)

store_quest_number > quest_number[X],0 # (store_quest_number,<destination>,<quest_id>),

store_quest_item > quest_item[X],0 # (store_quest_item,<destination>,<item_id>),

store_quest_troop > quest_troop[X],0 # (store_quest_troop,<destination>,<troop_id>),

store_current_hours > cur_hours[X] # (store_current_hours,<destination>),

store_time_of_day > cur_time_of_day[X] # (store_time_of_day,<destination>),

store_current_day > cur_day[X] # (store_current_day,<destination>),

store_distance_to_party_from_party > distance_parties[X],0,0 # (store_distance_to_party_from_party,<destination>,<party_id>,<party_id>),

get_party_ai_behavior > party_ai_behavior[X],0 # (get_party_ai_behavior,<destination>,<party_id>),

get_party_ai_object > party_ai_object[X],0 # (get_party_ai_object,<destination>,<party_id>),

#party_get_ai_target_position          = 2292	# (party_get_ai_target_position,<position_no>,<party_id>),

get_party_ai_current_behavior > cur_party_ai_behavior[X],0 # (get_party_ai_current_behavior,<destination>,<party_id>),

get_party_ai_current_object > cur_party_ai_object[X],0 # (get_party_ai_current_object,<destination>,<party_id>),

store_num_parties_created > num_created_parties[X],0 # (store_num_parties_created,<destination>,<party_template_id>),

store_num_parties_destroyed > num_destroyed_parties[X],0 # (store_num_parties_destroyed,<destination>,<party_template_id>),

store_num_parties_destroyed_by_player > num_destroyed_parties_by_player[X],0 # (store_num_parties_destroyed_by_player,<destination>,<party_template_id>),

# Searching operations.
store_num_parties_of_template > num_parties_of_template[X],0 # (store_num_parties_of_template,<destination>,<party_template_id>),

store_random_party_of_template > random_party_of_template[X],0 # fails if no party exists with tempolate_id (expensive)
#					# (store_random_party_of_template,<destination>,<party_template_id>),

#str_store_string                = 2320	# (str_store_string,<string_register>,<string_id>),
#str_store_string_reg            = 2321	# (str_store_string,<string_register>,<string_no>), #copies one string register to another.
#str_store_troop_name            = 2322	# (str_store_troop_name,<string_register>,<troop_id>),
#str_store_troop_name_plural     = 2323	# (str_store_troop_name_plural,<string_register>,<troop_id>),
#str_store_troop_name_by_count   = 2324	# (str_store_troop_name_by_count,<string_register>,<troop_id>,<number>),
#str_store_item_name             = 2325	# (str_store_item_name,<string_register>,<item_id>),
#str_store_item_name_plural      = 2326	# (str_store_item_name_plural,<string_register>,<item_id>),
#str_store_item_name_by_count    = 2327	# (str_store_item_name_by_count,<string_register>,<item_id>),
#str_store_party_name            = 2330	# (str_store_party_name,<string_register>,<party_id>),
#str_store_agent_name            = 2332	# (str_store_agent_name,<string_register>,<agent_id>),
#str_store_faction_name          = 2335	# (str_store_faction_name,<string_register>,<faction_id>),
#str_store_quest_name            = 2336	# (str_store_quest_name,<string_register>,<quest_id>),
#str_store_info_page_name        = 2337	# (str_store_info_page_name,<string_register>,<info_page_id>),
#str_store_date                  = 2340 # (str_store_date,<string_register>,<number_of_hours_to_add_to_the_current_date>),
#str_store_troop_name_link       = 2341 # (str_store_troop_name_link,<string_register>,<troop_id>),
#str_store_party_name_link       = 2342 # (str_store_party_name_link,<string_register>,<party_id>),
#str_store_faction_name_link     = 2343 # (str_store_faction_name_link,<string_register>,<faction_id>),
#str_store_quest_name_link       = 2344 # (str_store_quest_name_link,<string_register>,<quest_id>),
#str_store_info_page_name_link   = 2345 # (str_store_info_page_name_link,<string_register>,<info_page_id>),
#str_store_class_name            = 2346 # (str_store_class_name,<stribg_register>,<class_id>)
#str_store_player_username       = 2350 # (str_store_player_username,<string_register>,<player_id>), #used in multiplayer mode only
#str_store_server_password       = 2351 # (str_store_server_password, <string_register>),
#str_store_server_name           = 2352 # (str_store_server_name, <string_register>),
#str_store_welcome_message       = 2353 # (str_store_welcome_message, <string_register>),

#mission ones:
store_remaining_team_no > remaining_team_no[X]

# CONTINUE IN LINE 1315 OF header_operations.py !!!
