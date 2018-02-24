@echo off
python.exe process_init.py
python.exe process_global_variables.py
python.exe process_strings.py
python.exe process_skills.py
python.exe process_music.py
python.exe process_animations.py
python.exe process_meshes.py
python.exe process_sounds.py
python.exe process_skins.py
python.exe process_map_icons.py
python.exe process_factions.py
python.exe process_items.py
python.exe process_scenes.py
python.exe process_troops.py
python.exe process_particle_sys.py
python.exe process_scene_props.py
python.exe process_tableau_materials.py
python.exe process_presentations.py
python.exe process_party_tmps.py
python.exe process_parties.py
python.exe process_quests.py
python.exe process_info_pages.py
python.exe process_scripts.py
python.exe process_mission_tmps.py
python.exe process_game_menus.py
python.exe process_simple_triggers.py
python.exe process_dialogs.py
python.exe process_global_variables_unused.py
python.exe process_postfx.py
@del *.pyc
echo.
echo ______________________________
echo.
echo Script processing has ended.
echo Press any key to exit. . .
pause>nul