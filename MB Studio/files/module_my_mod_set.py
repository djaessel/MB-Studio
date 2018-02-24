from header_operations import *
from ID_items import *
from ID_troops import *
from ID_strings import *
from module_constants import *
from module_items import *
from header_item_modifiers import *

###################################################
# module_my_mod_set.py
# This file contains many defines of my own mod.
###################################################


def get_hrd_weight(y):
  a = (y >> ibf_weight_bits) & ibf_armor_mask
  return int(25 * a)

def set_item_data():
  item_data = []
  for i_item in xrange(len(items)):
    ## weight
    item_data.append((item_set_slot, i_item, slot_item_weight, get_hrd_weight(items[i_item][6])))
    
    ## difficulty
    item_data.append((item_set_slot, i_item, slot_item_difficulty, get_difficulty(items[i_item][6])))
    
    ## armor type
    if items[i_item][7] == imodbits_cloth:
      item_data.append((item_set_slot, i_item, slot_armor_type, armor_cloth))
    elif items[i_item][7] == imodbits_armor:
      item_data.append((item_set_slot, i_item, slot_armor_type, armor_armor))
    elif items[i_item][7] == imodbits_plate:
      item_data.append((item_set_slot, i_item, slot_armor_type, armor_plate))
    
    ## item_best_modifier
    if items[i_item][7] == imodbits_bow:
      item_data.append((item_set_slot, i_item, slot_item_best_modifier, imod_masterwork))
    elif items[i_item][7] == imodbits_pick:
      item_data.append((item_set_slot, i_item, slot_item_best_modifier, imod_balanced))
    elif items[i_item][7] == imodbits_none:
      item_data.append((item_set_slot, i_item, slot_item_best_modifier, imod_plain))
    else:
      for i in xrange(43):
        if items[i_item][7] >> i == 1:
          item_data.append((item_set_slot, i_item, slot_item_best_modifier, i))
    
    type = items[i_item][3] & 0x000000ff
    if type == itp_type_two_handed_wpn and items[i_item][3] & itp_two_handed == 0:
      item_data.append((item_set_slot, i_item, slot_item_two_hand_one_hand, 1))
    
    if items[i_item][3] & itp_cant_use_on_horseback == itp_cant_use_on_horseback:
      item_data.append((item_set_slot, i_item, slot_item_cant_on_horseback, 1))
    
    if type >= itp_type_head_armor and type <= itp_type_hand_armor:
      item_data.append((item_set_slot, i_item, slot_item_head_armor, get_head_armor(items[i_item][6])))
      item_data.append((item_set_slot, i_item, slot_item_body_armor, get_body_armor(items[i_item][6])))
      item_data.append((item_set_slot, i_item, slot_item_leg_armor, get_leg_armor(items[i_item][6])))
    elif (type >= itp_type_one_handed_wpn and type <= itp_type_thrown and type != itp_type_shield) or (type >= itp_type_pistol and type <= itp_type_bullets):
      item_data.append((item_set_slot, i_item, slot_item_thrust_damage, get_thrust_damage(items[i_item][6])))
      item_data.append((item_set_slot, i_item, slot_item_swing_damage, get_swing_damage(items[i_item][6])))
      item_data.append((item_set_slot, i_item, slot_item_speed, get_speed_rating(items[i_item][6])))
      item_data.append((item_set_slot, i_item, slot_item_length, get_weapon_length(items[i_item][6])))
    elif type == itp_type_horse:
      item_data.append((item_set_slot, i_item, slot_item_horse_speed, get_missile_speed(items[i_item][6])))
      item_data.append((item_set_slot, i_item, slot_item_horse_armor, get_body_armor(items[i_item][6])))
      item_data.append((item_set_slot, i_item, slot_item_horse_charge, get_thrust_damage(items[i_item][6])))
    elif type == itp_type_shield:
      item_data.append((item_set_slot, i_item, slot_item_length, get_weapon_length(items[i_item][6])))
      item_data.append((item_set_slot, i_item, slot_item_body_armor, get_body_armor(items[i_item][6])))
      item_data.append((item_set_slot, i_item, slot_item_speed, get_speed_rating(items[i_item][6])))
    
  ## item_modifier
  for i_modifier in xrange(len(modifiers)):
    item_data.append((item_set_slot, i_modifier, slot_item_modifier_multiplier, modifiers[i_modifier][1]))
    item_data.append((item_set_slot, i_modifier, slot_item_modifier_quality, modifiers[i_modifier][2]))
    
  return item_data[:]

modifiers = [
  (imod_plain, 100, 0), 
  (imod_cracked, 50, -1), 
  (imod_rusty, 55, -1), 
  (imod_bent, 65, -1), 
  (imod_chipped, 72, -1), 
  (imod_battered, 75, -1), 
  (imod_poor, 80, -1), 
  (imod_crude, 83, -1), 
  (imod_old, 86, -1), 
  (imod_cheap, 90, -1), 
  (imod_fine, 190, 1), 
  (imod_well_made, 250, 1), 
  (imod_sharp, 160, 1), 
  (imod_balanced, 350, 1), 
  (imod_tempered, 670, 1), 
  (imod_deadly, 850, 1), 
  (imod_exquisite, 1450, 1), 
  (imod_masterwork, 1750, 1), 
  (imod_heavy, 190, 1), 
  (imod_strong, 490, 1), 
  (imod_powerful, 320, 1), 
  (imod_tattered, 50, -1), 
  (imod_ragged, 70, -1), 
  (imod_rough, 60, -1), 
  (imod_sturdy, 170, 1), 
  (imod_thick, 260, 1), 
  (imod_hardened, 390, 1), 
  (imod_reinforced, 650, 1), 
  (imod_superb, 250, 1), 
  (imod_lordly, 1150, 1), 
  (imod_lame, 40, -1), 
  (imod_swaybacked, 60, -1), 
  (imod_stubborn, 90, 1), 
  (imod_timid, 180, 1), 
  (imod_meek, 180, -1), 
  (imod_spirited, 650, 1), 
  (imod_champion, 1450, 1), 
  (imod_fresh, 100, 1), 
  (imod_day_old, 100, -1), 
  (imod_two_day_old, 90, -1), 
  (imod_smelling, 40, -1), 
  (imod_rotten, 5, -1), 
  (imod_large_bag, 190, 1)
]

def keys_array():
  keys_list = []
  for key_no in xrange(len(keys)):
    keys_list.append((troop_set_slot, "trp_temp_array_a", key_no, keys[key_no]))
    keys_list.append((troop_set_slot, "trp_temp_array_b", key_no, str_key_0+key_no))
  return keys_list[:]
  
keys = [key_0, key_1, key_2, key_3, key_4, key_5, key_6, key_7, key_8, key_9, key_a, key_b, key_c, key_d, key_e, key_f, key_g, key_h, key_i, key_j, key_k, key_l, key_m, key_n, key_o, key_p, key_q, key_r, key_s, key_t, key_u, key_v, key_w, key_x, key_y, key_z, key_numpad_0, key_numpad_1, key_numpad_2, key_numpad_3, key_numpad_4, key_numpad_5, key_numpad_6, key_numpad_7, key_numpad_8, key_numpad_9, key_num_lock, key_numpad_slash, key_numpad_multiply, key_numpad_minus, key_numpad_plus, key_numpad_enter, key_numpad_period, key_insert, key_delete, key_home, key_end, key_page_up, key_page_down, key_up, key_down, key_left, key_right, key_f1, key_f2, key_f3, key_f4, key_f5, key_f6, key_f7, key_f8, key_f9, key_f10, key_f11, key_f12, key_space, key_escape, key_enter, key_tab, key_back_space, key_open_braces, key_close_braces, key_comma, key_period, key_slash, key_back_slash, key_equals, key_minus, key_semicolon, key_apostrophe, key_tilde, key_caps_lock, key_left_shift, key_right_shift, key_left_control, key_right_control, key_left_alt, key_right_alt]