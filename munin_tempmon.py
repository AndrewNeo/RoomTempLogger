#!/usr/bin/python

import os;
import sys;
import socket;
import pymetar;

# Zone is your NOAA station ID (see http://www.nws.noaa.gov/tg/siteloc.shtml)
Zone = "";
DataIP = "";
DataPort = 5348;

# Show config
if ((len(sys.argv) > 1) and (sys.argv[1] == "config")):
        print "graph_title Temperature Monitor";
        print "graph_vlabel Temperature";
        print "graph_info Room temperature monitor.";
        print "graph_category Sensors";
        print "graph_args --base 1000";
        print "graph_scale no"
        print "light.label Light sensor"
        print "light.info Light sensor data."
        print "light.draw AREA"
        print "light.color EEFFCC"
        print "roomtemp.label Room temp (F)"
        print "roomtemp.info Room temperature in F."
        print "roomtemp.draw LINE"
        print "roomtemp.color FF0000"
        print "outtemp.label Outdoor temp (F)"
        print "outtemp.info Outdoor temperature in F."
        print "outtemp.draw LINE"
        print "outtemp.color 0000FF"
        sys.exit(0);

# Get indoor temp and light
dataresult = "";

try:
        s = socket.socket(socket.AF_INET, socket.SOCK_STREAM);
        s.connect((DataIP, DataPort));

        data = s.recv(1024);
        #dataresult = repr(data)

        s.close();

        dsplit = data.split(",");

        rtemp = float(dsplit[1]);
        lightval = int(dsplit[2]);
        lightpct = round((lightval / 1024.0) * 100, 2);
except socket.error:
        rtemp = 0;
        lightpct = 0;

# Get outdoor temp
fetcher = pymetar.ReportFetcher(Zone);
report = fetcher.FetchReport();
parser = pymetar.ReportParser(report);
parser.ParseReport();
otemp = report.getTemperatureFahrenheit();

# Output results
print "light.value %s" % lightpct;
print "roomtemp.value %s" % rtemp;
print "outtemp.value %s" % otemp;