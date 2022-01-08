% Read Wisconsin Breast Cancer database
% import Data
clear; clc; close all;

fid = fopen('data-after-edit.csv','r');
Data = textscan(fid, ['%d,%[^,],',repmat('%f,',1,8),'%f\n']);
fclose(fid);

n = length(Data);
n = n-2;
P = length(Data{1});
%% prepare Data sets
x = zeros(P,n);
for pat=1:P
    for i=1:n
        x(pat,i) = double(Data{i+2}(pat));
    end
end

t = zeros(1,P);
for i=1:P
    if char(Data{2}(i)) == 'M'
        t(i) = 1;
    else
        t(i) = 0;
    end
end
t = t';
%% storage
save('wdbc.mat', 'x', 't');
load ('wdbc.mat');

x = x';
t=t';
%% Prepare Neural Network re-train multiple times with different algorithms
    hidden_layers_neurons = [10];
    
    training_function = 'trainscg'; % trainscg , trainrp, traingdx
        
    net = patternnet(hidden_layers_neurons);
    net.trainFcn = training_function;
    net.layers{1}.transferFcn = 'logsig'; % tansig
    net.trainParam.epochs = 1500;
    net.trainParam.show = 1;

    net.divideParam.trainRatio = 70/100;
    net.divideParam.valRatio = 15/100;
    net.divideParam.testRatio = 15/100;

    net.trainParam.max_fail = 10;

    % traingdx only
    %net.trainParam.lr = 0.1; 
    %net.trainParam.mc = 0.4; 

    net = init(net);

    [net,tr] = train(net, x, t); %train