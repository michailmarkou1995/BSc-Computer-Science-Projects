% Read Wisconsin Breast Cancer database
clear; clc; close all;

fid = fopen('data.csv','r');
Data = textscan(fid, ['%d,%[^,],',repmat('%f,',1,29),'%f\n'], 'HeaderLines',1);
fclose(fid);

%fid = fopen('data.csv','r');
%formatspec = '%s%s';
%textData = textscan(fid,formatspec,'Delimiter', '%[^""]');
%textData = regexprep(textData{2}, '\s', '');
%textData = textData(1:32,1:1); % whole feature data
%textFeatures = textData(3:32,1:1); % filter from features x/p input mapping
%fclose(fid);

n = length(Data);       % number of features + target
n = n-2;                % ignore first two attributes (ID, target)
P = length(Data{1});    % number of patterns
%% prepare Data sets
x = zeros(P,n);
for pat=1:P
    for i=1:n
        x(pat,i) = double(Data{i+2}(pat)); % fill row by row entering Data cell variable
    end
end

% row results if need to transpose to column use t = t'; or t = t.';
t = zeros(size(x,1),2); 
%t = zeros(1,P); % targets
for i=1:P
    if char(Data{2}(i)) == 'M'
        %t(pat) = 1; % Malignant
        t(i,:) = [1 0];
    else
        %t(pat) = -1; %Benign
        t(i,:) = [0 1];
    end
end
%t = t';
%save('wdbc.mat', 'x', 't', 'textFeatures');
save('wdbc.mat', 'x', 't');
% load ('wdbc.mat');


hidden_layers_neurons = [5];
training_function = 'trainscg';
net = patternnet(hidden_layers_neurons);
%net.divideFcn = 'dividerand';
net.trainFcn = training_function;
%net.trainParam.goal = 1e-4;
%net.trainParam.epochs = 1500;
net.trainParam.show = 1;
%net.performFcn = 'mse';

net.divideParam.trainRatio = 70/100;
net.divideParam.valRatio = 15/100;
net.divideParam.testRatio = 15/100;

net.trainParam.max_fail = 6;

%net.trainParam.lr = 0.1;
%net.trainParam.mc = 0.4;

net = init(net);

x = x(1:569,1:10);
 x = x';
 t=t';
[net,tr] = train(net, x, t);

%for i=1:100
outputs = net(x);
%end

figure, plotperform(tr);
figure, plotconfusion(t, outputs);