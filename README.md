# Beep Player 系統蜂鳴器鋼琴 🎹

這是一個使用 C# Windows Forms 開發的趣味小工具。透過呼叫 Windows 系統底層的 API (`kernel32.dll`)，利用主機板或系統音效發出不同頻率的聲音，在畫面上模擬出一個可以彈奏 C4 到 C5 八度音階的迷你鍵盤。

## 功能特色 (Features)

* **系統發聲 (System Beep API):** 不需依賴外部音效檔，直接使用 P/Invoke 呼叫 `Beep` 函式發聲。
* **八度音階鍵盤:** 內建 8 個按鍵，精準對應 C4 到 C5 的頻率 (523Hz ~ 1046Hz)。透過共用事件處理函式與 `TabIndex` 映射，讓程式碼更簡潔。
* **響應式視窗 (Responsive UI):** 視窗大小改變時，內部按鍵會依照初始比例動態自動縮放，確保在任何視窗尺寸下都能保持完美的版面配置。
* **防呆與防誤觸機制:** * 視窗縮放事件具備防呆檢查，確保控制項初始化完成且存在於紀錄字典中才進行計算，避免程式崩潰。
  * 點擊視窗右上角關閉時，會觸發 `FormClosing` 跳出確認對話方塊。

## 開發環境與技術 (Tech Stack)

* **程式語言:** C#
* **應用程式框架:** Windows Forms (.NET)
* **底層呼叫:** Windows API (`DllImport("kernel32.dll")`)
* **開發工具:** Visual Studio 2022

## 執行畫面 (Screenshots)

> **操作提示：** 將程式執行時的截圖命名為 `screenshot.png`，並上傳至此 GitHub 專案的根目錄中，圖片就會自動顯示在下方。

![BeepPlayer 執行畫面](./screenshot.png)

## 執行與編譯說明 (Getting Started)

### 先決條件
請確保您的電腦已安裝 [Visual Studio 2022](https://visualstudio.microsoft.com/zh-hant/vs/)，並包含「.NET 桌面開發」工作負載。

### 執行步驟
1. 複製此專案到本地端：
   ```
   git clone [https://github.com/Thomas-debuger/BeepPlayer.git](https://github.com/Thomas-debuger/BeepPlayer.git)
   ```

2. 進入專案資料夾，對著 `BeepPlayer.sln` 方案檔點擊兩下，以 Visual Studio 開啟專案。
3. 在 Visual Studio 中，按下鍵盤上的 `F5` 鍵，或點擊上方工具列的 **[啟動]** 按鈕。
4. 程式執行後：
* 使用滑鼠點擊畫面上的按鈕，即可聽到不同頻率的音符。
* 嘗試拖曳視窗邊緣改變大小，按鈕會自動等比例縮放。



## 核心程式碼架構

**1. 呼叫系統 Beep API**

```csharp
[DllImport("kernel32.dll")]
public static extern bool Beep(int frequency, int duration);

```

**2. 響應式縮放邏輯**
透過 `Dictionary` 紀錄控制項 (`Rect`) 的初始長寬與位置，並在 `SizeChanged` 事件中計算比例動態調整：

```csharp
double iRatioWith = width / this.initWidth;
double iRatioHeight = height / this.initHeight;
// 依照 iRatioWith 與 iRatioHeight 重新設定控制項的 Width、Height、Top 與 Left

```
