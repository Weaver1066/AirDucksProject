BROADCAST_TO_PORT = 12000
import time
from socket import *
from datetime import datetime
from sense_hat import SenseHat

sense = SenseHat()
serverName = "192.168.24.173"
s = socket(AF_INET, SOCK_STREAM)
#s.bind(('', 14593))     # (ip, port)
# no explicit bind: will bind to default IP + random port
s.connect((serverName, 12000))

while True:
    data = "current temperature: " + str(sense.get_temperature())
    s.send(data.encode())
    print(data)
    time.sleep(1)
