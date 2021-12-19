% Read Wisconsin Breast Cancer database
% import Data
clear; clc; close all;

fid = fopen('data.csv','r');
Data = textscan(fid, ['%d,%[^,],',repmat('%f,',1,29),'%f\n'], 'HeaderLines',1); % first col is decimal second is string then the rest 29 is features and throw/delete headeline first row of csv text
fclose(fid);

n = length(Data);       % number of features + target
n = n-2;                % ignore first two attributes/Features (ID of patient, target of cancer Type)
P = length(Data{1});    % number of patterns/samples
%% prepare Data sets
x = zeros(P,n);
for pat=1:P
    for i=1:n
        x(pat,i) = double(Data{i+2}(pat)); % fill row by row entering Data cell variable
    end
end

%t = zeros(size(x,1),2); 
t = zeros(1,P); % targets % use this if uncommented line 21,24,27,31 and 20,25,28 commented or reverse it for the other way
for i=1:P
    if char(Data{2}(i)) == 'M'
        t(i) = 1; % Malignant
        %t(i,:) = [1 0];
    else
        t(i) = 0; %Benign ###### ----> cant use -1 why? i use [1] or [0] output instead of [1 0] [0 1] but cant [1] [-1]
        %t(i,:) = [0 1];
    end
end
t = t'; % or t = t.'; % transpose in order for proper dimensions inputs & targets to process later

%% storage
%save('wdbc.mat', 'x', 't');
%load ('wdbc.mat');

%% Prepare Neural Network

% every iteration new training function tested numerous times and getting average results of the mse
for test_various_training_functions_average_results = 1:3
    hidden_layers_neurons = [10];
    if (test_various_training_functions_average_results == 1)
        training_function = 'trainscg';
    end
    if (test_various_training_functions_average_results == 2)
        training_function = 'trainrp';
    end
    if (test_various_training_functions_average_results == 3)
        training_function = 'traingdx';
    end
    
    net = patternnet(hidden_layers_neurons); %feedforwardnet for fitting not classification
    %net = setwb(net,rand(55,1));
    %net = setwb(net,1);
    %net.IW{1,1}
    %net.b{1}
    %net.b = [2 1; 1];
    %view(net);
    %net.divideFcn = 'dividerand';
    net.trainFcn = training_function;
    net.layers{1}.transferFcn = 'logsig'; % tansig
    %net.trainParam.goal = 1e-2;
    net.trainParam.epochs = 1500;
    net.trainParam.show = 1;
    net.performFcn = 'mse'; % or crossentropy

    net.divideParam.trainRatio = 70/100;
    net.divideParam.valRatio = 15/100;
    net.divideParam.testRatio = 15/100;

    net.trainParam.max_fail = 10; % when validation should kick in by stopping the process of the neural network

    % traingdx only
    if (strcmp(training_function, 'traingdx'))  
        % In setting a learning rate, there is a trade-off between the rate of convergence and overshooting.
        net.trainParam.lr = 0.1; %0.1 0.4; % Corresponds how fast will move (step size at each iteration) towards goal (minimum of loss function)
        net.trainParam.mc = 0.4; %0.4 0.9; % Corresponds how strong (control adaptation parameter) will be the influence of the direction (training gain) that is heading towards (affects error convergence)
    end

    net = init(net);
    
    % transpose only on first run the matrix cells
    if (test_various_training_functions_average_results == 1)
         x = x(1:569,1:10); % Feature selection only the first 10 columns of 569 samples
         x = x';
         t=t';
    end

    [net,tr] = train(net, x, t); %train

    %% Confusion Matrix

    outputs= zeros(length(x(:,:)),length(x(1,:))); % fill with 569x569
    pcorrect=0;
    perror=0;

    % Average re-train confusion
    mse=0.0;
    for i=1:15 % 15 times re-train
        [net,tr] = train(net, x, t); % re-train
        outputs = net(x);
        Nepochs = tr.epoch(end); % Total epochs iteration of net train
        N = length(x); % Proccessed input Data Length
        for i_epoch = 1:Nepochs % iterations run (total number of epochs trained)
            for i_mse = 1:N % do addititive summary 569 times row (1 epoch) then divide it with 569.. do that for Nepochs
                 mse(1) = mse(1) + mean((t(i_mse)-outputs(i_mse)).^2); % add in first col the previous mse + next the row-by-row actual target from Data - Data from output you got instead
            end
            mse(1) = mse(1)/N;
        end
        [c,cm,ind,per] = confusion(t,outputs);
        pcorrect = pcorrect + (100*(1-c)); % numeric representation of Correct Classification
        perror = perror + (100*c); % numeric representation of Incorrect Classification
        if (i==15)
            pcorrect = pcorrect / 15;
            perror = perror / 15;
            fprintf('%s = Average Percentage Total Correct Classification   : %f%% | MSE : %f%%\n', training_function, pcorrect, mse(1));
            fprintf('%s = Average Percentage Total Incorrect Classification : %f%%\n', training_function, perror);
        end
    end
end

%% Draft Area

% for i = 1:N-1
%     mse_graph(i) = mean((t(i)-outputs(i)).^2);
% end
%plot(abs(mse_graph));

%shoi = regexprep(shoi,'[^?:\.A-Za-z1-9]',''); % keep only numbers with . start from 1
%shoi_n = shoi_n + str2double(shoi);

% % CALCULATE MSE
% mse=0.0;
% Nepochs = tr.epoch(end); % Total epochs iteration of net train
% N = length(x); % Proccessed input Data Length
% for i_epoch = 1:Nepochs % iterations run (total number of epochs trained)
%     for i_mse = 1:N % do addititive summary 569 times row (1 epoch) then divide it with 569.. do that for Nepochs
%          mse(1) = mse(1) + mean((t(i_mse)-outputs(i_mse)).^2); % add in first col the previous mse + next the row-by-row actual target from Data - Data from output you got instead
%     end
%     mse(1) = mse(1)/N;
% end
% fprintf('last mse output train : %f%%\n', mse(1));
