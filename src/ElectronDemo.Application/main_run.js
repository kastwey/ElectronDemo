﻿const { app, BrowserWindow } = require('electron')
const path = require('path')
const url = require('url')

// Keep a global reference of the window object, if you don't, the window will
// be closed automatically when the JavaScript object is garbage collected.
let win

function createWindow() {
	
var spawn = require('child_process').spawn;
var executablePath = path.join(__dirname, 'ElectronDemo.Application.exe');

const theProcess = spawn(executablePath, [],
{ cwd: __dirname,
shell: false });

theProcess.stdout.on('data', (data) => {
  console.log(`stdout: ${data}`);
});

theProcess.stderr.on('data', (data) => {
  console.log(`stderr: ${data}`);
});

theProcess.on('close', (code) => {
  console.log(`child process exited with code ${code}`);
});

console.log("SE supone que ya se está ejecutando.");

	setTimeout(function () {
		// Create the browser window.
		win = new BrowserWindow({ width: 800, height: 600 })

		// and load the index.html of the app.
		win.loadURL('http://localhost:50693/'
		);
		// Open the DevTools.
	// win.webContents.openDevTools()

		// Emitted when the window is closed.
		win.on('closed', () => {
			// Dereference the window object, usually you would store windows
			// in an array if your app supports multi windows, this is the time
			// when you should delete the corresponding element.
			win = null
		})
	}, 3000);

	


}

// This method will be called when Electron has finished
// initialization and is ready to create browser windows.
// Some APIs can only be used after this event occurs.
app.on('ready', createWindow)

// Quit when all windows are closed.
app.on('window-all-closed', () => {
	// On macOS it is common for applications and their menu bar
	// to stay active until the user quits explicitly with Cmd + Q
	if (process.platform !== 'darwin') {
		app.quit()
	}
})

app.on('activate', () => {
	// On macOS it's common to re-create a window in the app when the
	// dock icon is clicked and there are no other windows open.
	if (win === null) {
		createWindow()
	}
})

// In this file you can include the rest of your app's specific main process
// code. You can also put them in separate files and require them here.