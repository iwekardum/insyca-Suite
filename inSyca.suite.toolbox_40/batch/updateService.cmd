net stop "inSyca MessageBroker"
xcopy "\\SERVERNAME\shared\service\update\service\*.*" "C:\Program Files (x86)\inSyca Messagebroker\*.*" /e /i /y /s
net start "inSyca MessageBroker"
PAUSE