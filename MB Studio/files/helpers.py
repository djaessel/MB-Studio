#HELPER START

#HELPER1
def swing_damage(damage,damage_type):
  x = ((damage_type << iwf_damage_type_bits) | (damage & ibf_armor_mask))
  return (((bignum | x) & ibf_damage_mask) << iwf_swing_damage_bits)

def get_swing_damage(y):
  return (y >> iwf_swing_damage_bits) & ibf_damage_mask

def thrust_damage(damage,damage_type):
  x = ((damage_type << iwf_damage_type_bits) | (damage & ibf_armor_mask))  # x = ((1 << 8) | (0x13 & 0x00000000000000000000000ff)) --> x = (0x100 | 0x13) --> x = 0x113 = 275 = 0b100010011
  return (((bignum | x) & ibf_damage_mask) << iwf_thrust_damage_bits)

def get_thrust_damage(y):
  return (y >> iwf_thrust_damage_bits) & ibf_damage_mask
#HELPER1

#HELPER2
def hit_points(x):
  return (((bignum | x) & ibf_hitpoints_mask) << ibf_hitpoints_bits) # --> (((bignum | x) & 0x0000ffff) << 40) --> (0x40000000000000000000000000000000 | )

def get_hit_points(y):
  return (y >> ibf_hitpoints_bits) & ibf_hitpoints_mask # --> (y >> 40) & 0x0000ffff

#HELPER2

#HELPER END
