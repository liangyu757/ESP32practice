# Dungeon Defense

Tower Defense × Roguelite × RPG 手機遊戲（iOS 優先）。

- Engine: Unity 6 LTS，Universal 2D (URP)
- Language: C#
- 目前階段：**Phase 0 — 專案基礎架構**

## 專案結構

```
Assets/_Project/
├── Scenes/                Boot, MainMenu, Town, Battle（由編輯器工具產生）
├── Scripts/               DungeonDefense.asmdef
│   ├── Core/              GameState, GameStateMachine, GameManager, SceneLoader, SceneNames
│   ├── Data/              GameData 基底 + HeroData, TowerData, EnemyData, SkillData, MapData
│   ├── DevTools/          GameStateDebugPanel（僅開發版顯示）
│   └── Editor/            ProjectSetup（建立 Scene 與 Build Settings）
├── Tests/EditMode/        GameStateMachine 與 Data 測試
├── ScriptableObjects/     Heroes/ Towers/ Enemies/ Skills/ Maps/（資料 asset）
├── Prefabs/  Art/  Audio/
```

所有專案檔案放在 `_Project/` 底下，與第三方套件分開。

## 架構原則

- **Data 與 Logic 分離**：`XxxData`（ScriptableObject，只存數值）↔ `XxxController`（MonoBehaviour，行為）。
- Data 在執行期間為唯讀，避免遊戲中意外修改共用 asset。
- `GameStateMachine` 是純 C#，不依賴 Unity，可直接做單元測試。
- `GameManager` 只負責狀態切換（載入 Scene、暫停時間），不放任何遊戲玩法邏輯。
- 系統在實際需要時才加入（AudioManager、SaveManager 延後）。

## 遊戲狀態

```
Boot → MainMenu → Town ⇄ Battle → Reward → Town
                          Battle → Pause → Battle / Town
                          Battle → GameOver → Town / MainMenu
```

允許的轉換全部定義在 `GameStateMachine.cs` 的表格中。新增狀態：在 `GameState` 加入值，並在表格加入轉換。

Pause、Reward、GameOver 會覆蓋在目前的 Scene 上，不會另外載入 Scene。

## 第一次設定（在 Mac 上）

1. 用 Unity Hub 建立 **Universal 2D** 專案（Unity 6 LTS，並安裝 iOS Build Support）。
2. 將本 repo 的 `Assets/_Project/`、`.gitignore`、`.gitattributes` 複製到專案根目錄。
3. 開啟 Unity，選單 **Dungeon Defense → Setup → Create Core Scenes**。
   - 建立 4 個 Scene，並以 Boot 為第一個加入 Build Settings。
   - 範本內的 `Assets/Scenes/SampleScene.unity` 可以刪除。
4. 按 Play：Play Mode 一律從 Boot 開始，左上角的 Debug Panel 可以切換狀態。
5. **Window → General → Test Runner → EditMode → Run All**。

## 新增遊戲資料

在 Project 視窗按右鍵 **Create → Dungeon Defense → Hero / Tower / Enemy / Skill / Map Data**，
將 asset 放到 `ScriptableObjects/` 對應的資料夾。
