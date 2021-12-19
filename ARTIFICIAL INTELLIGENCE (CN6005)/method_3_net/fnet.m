clear; clc; close all;
%[p, t] = simplefit_dataset;
load wdbc.mat;
p=x';
t=t';
p=p(1,:); % keep first row with all colmns

plot(p,t,'*');
net = feedforwardnet(6);
% net.numLayers = 2;
% net.layers[1].size = 8;
% net.layers[1].size = 5;

net.trainParam.goal = 1e-5;
net.trainParam.epochs = 1000;

net.divideParam.trainRatio = 60/100;
net.divideParam.valRatio = 0/100;
net.divideParam.testRatio = 40/100;

[net, tr] = train(net, p, t);

p_sim = p;
t_sim = net(p_sim);
hold on;
plot(p_sim, t_sim, 'r-');
hold off;